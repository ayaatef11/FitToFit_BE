using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernal.BuildingBlocks;
using SharedKernal.Presistance.DbConnection;
using SharedKernal.Syncronization.Cancelation;
using System.Data;
using System.Data.Common;

namespace SharedKernal.UnitOfWork
{
    public sealed class UOF(
        ILogger<UOF> logger,IDbConnectionFacory connectionFactory,ICancelationTokenFactory cancellationTokenFactory,
        IMediator mediator) : IUOF
    {
        private readonly ILogger<UOF> logger = logger;
        public DbConnection Connection { get; set; } = connectionFactory?.CreateConnection();

        #region Private Variables

        private IsolationLevel _dbTransactionIsolationLevel = IsolationLevel.ReadCommitted;
        private bool _dbConnectionOpenedLocally;
        private DbTransaction _transaction = null;
        private readonly object _lockObject = new();//for multiple users accessing at the same time 
        private int _dbTransactionNestLevel = -1;
        private readonly HashSet<DbContext> _dbContexts = [];//logical partitioning of the databases 
        private readonly CancellationToken _cancellationToken = cancellationTokenFactory.GetCancellationToken();
        private readonly IMediator _mediator = mediator;
        private const int MaxRecursionDepth = 10; //Don't Change this number before consulting with the team leader
        // when dealing with hierarchical or deeply related entities, where one entity references another
        #endregion

        #region IUnitOfWork Implementation

        public async Task<T> UsingStrategy<T>(Func<Task<T>> action)
        {
            var strategy = _dbContexts.First().Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                return await action();
            });
        }
        public void Begin()
        {
            Begin(IsolationLevel.ReadCommitted);//not _dbTransactionIsolationLevel because it may be changed using the next begin function 
        }

        public void Begin(IsolationLevel isolationLevel)
        {
            lock (_lockObject)
            {
                if (_transaction == null)
                {
                    _dbTransactionIsolationLevel = isolationLevel;
                    OpenConnection();
                    _transaction = Connection.BeginTransaction(_dbTransactionIsolationLevel);
                    foreach (var dbContext in _dbContexts) dbContext.Database.UseTransaction(_transaction);
                }

                _dbTransactionNestLevel++;
            }
        }

        public async Task Commit()
        {
            if (_dbTransactionNestLevel > 0)
            {
                _dbTransactionNestLevel--;
                return;
            }

            try
            {
                await PublishDomainEvents();
                await _transaction.CommitAsync(_cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error happened during commiting the changes by unitOfWork , going to rollback now ... ");

                await Rollback();

                throw;
            }
            finally
            {
                Reset();
            }
        }

        public async Task Rollback()
        {
            if (_dbTransactionNestLevel == -1)
            {
                logger.LogInformation("metigate rolling back again");
                return;
            }

            try
            {
                await _transaction.RollbackAsync(_cancellationToken);
            }
            finally
            {
                Reset();
            }
        }

        #endregion

        #region Helpers

        private void Reset()
        {
            _transaction.Dispose();
            _dbTransactionIsolationLevel = IsolationLevel.ReadCommitted;
            _dbTransactionNestLevel = -1;
            CloseConnection();
        }

        private void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open && _dbConnectionOpenedLocally)
            {
                Connection.Close();
                _dbConnectionOpenedLocally = false;
            }
        }

        private void OpenConnection()
        {
            if (Connection.State == ConnectionState.Open) return;

            Connection.Open();
            _dbConnectionOpenedLocally = true;
        }

        public void RegisterContext(DbContext context)
        {
            lock (_lockObject)
            {
                _dbContexts.Add(context);
                if (_transaction != null) context.Database.UseTransaction(_transaction);
            }
        }

        private async Task PublishDomainEvents()
        {
            var eventsToPublish = new List<INotification>();

            CollectDomainEvents(eventsToPublish);

            var recursionDepth = 0;

            while (eventsToPublish.Any() && recursionDepth < MaxRecursionDepth)
            {
                recursionDepth++;

                foreach (var domainEvent in eventsToPublish)
                    await _mediator.Publish(domainEvent, _cancellationToken);

                eventsToPublish.Clear();
                CollectDomainEvents(eventsToPublish);
            }

            if (recursionDepth >= MaxRecursionDepth)
                throw new InvalidOperationException("Maximum recursion depth reached while publishing domain events.");
        }

        private void CollectDomainEvents(List<INotification> eventsToPublish)
        {
            foreach (var context in _dbContexts)
            {
                var aggregateRoots = context.ChangeTracker.Entries<IRootEntity>()
                    .Where(e => e.Entity.UncommittedEvents.Any())
                    .Select(e => e.Entity);

                foreach (var aggregateRoot in aggregateRoots)
                {
                    var domainEvents = aggregateRoot.UncommittedEvents.ToList();
                    aggregateRoot.ClearDomainEvents();
                    eventsToPublish.AddRange(domainEvents);
                }
            }
        }

        #endregion
    }
}
