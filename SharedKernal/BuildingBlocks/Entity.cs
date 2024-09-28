using KellermanSoftware.CompareNetObjects;
using System.Collections.ObjectModel;

namespace SharedKernal.BuildingBlocks
{
    public abstract class Entity<TEntity, TId> : IEntity<TId> where TEntity : Entity<TEntity, TId>
    {
        public required TId Id { get; set; }//place holder for any id type 

        #region Equals Implementation

        private static readonly CompareLogic Compare =
            new CompareLogic(new ComparisonConfig() { CompareChildren = true });

        private int? _hashCode;

        public static bool operator !=(Entity<TEntity, TId> entity1, Entity<TEntity, TId> entity2)
        {
            return !(entity1 == entity2);
        }

        public static bool operator ==(Entity<TEntity, TId> entity1, Entity<TEntity, TId> entity2)
        {
            if (ReferenceEquals(entity1, entity2)) return true;
            if (ReferenceEquals(entity1, null)) return false;
            if (ReferenceEquals(entity2, null)) return false;

            return entity1.Equals(entity2);
        }

        public override bool Equals(object obj)
        {
            if (obj is not TEntity otherObj)
                return false;

            var thisIsNewEntity = EqualityComparer<TId>.Default.Equals(Id, default(TId));
            var otherIsNewEntity = EqualityComparer<TId>.Default.Equals(otherObj.Id, default(TId));

            if (thisIsNewEntity && otherIsNewEntity)
                return ReferenceEquals(this, otherObj) || DeepEquals(this, otherObj);

            return Id.Equals(otherObj.Id);
        }

        public override int GetHashCode()
        {
            if (_hashCode.HasValue) return _hashCode.Value;

            var isNewEntity = EqualityComparer<TId>.Default.Equals(Id, default(TId));
            _hashCode = isNewEntity ? base.GetHashCode() : Id.GetHashCode();

            return _hashCode.Value;
        }

        private bool DeepEquals<T>(T thisObject, T otherObj)
        {
            return Compare.Compare(thisObject, otherObj).AreEqual;
        }

        #endregion Equals Implementation
    }
    public abstract class RootEntity<TId> : IRootEntity
    {
        public TId Id { get; set; }
        public static bool operator !=(RootEntity<TId> entity1, RootEntity<TId> entity2)
        {
            return !(entity1 == entity2);
        }

        public static bool operator ==(RootEntity<TId> entity1, RootEntity<TId> entity2)
        {
            if (ReferenceEquals(entity1, entity2)) return true;
            if (ReferenceEquals(entity1, null)) return false;
            if (ReferenceEquals(entity2, null)) return false;

            return entity1.Equals(entity2);
        }
        public override bool Equals(object obj)
        {
            if (obj is not RootEntity<TId> otherObj)
                return false;

            return Id.Equals(otherObj.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #region Private Fields

        protected readonly ICollection<IDomainEvent> _UncommittedEvents;

        #endregion Private Fields

        #region Constructors
        protected RootEntity()
        {
            _UncommittedEvents = new Collection<IDomainEvent>();
        }
        #endregion

        #region IAggregateRoot Implementation



        public void AddEvent(IDomainEvent domainEvent)
        {
            _UncommittedEvents.Add(domainEvent);
        }

        public void AddEvents(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                AddEvent(domainEvent);
            }
        }

        public void ClearDomainEvents()
        {
            _UncommittedEvents.Clear();
        }

        public bool IsEventAdded<TEvent>() where TEvent : IDomainEvent
        {
            if (_UncommittedEvents == null || _UncommittedEvents.Count == 0) return false;
            return _UncommittedEvents.OfType<TEvent>().Any();
        }

        public bool IsEventAdded(IDomainEvent domainEvent)
        {
            if (_UncommittedEvents == null || _UncommittedEvents.Count == 0) return false;
            return _UncommittedEvents.Any(e => Equals(e, domainEvent));
        }

        public IEnumerable<IDomainEvent> UncommittedEvents => new ReadOnlyCollection<IDomainEvent>(_UncommittedEvents.ToList());

        #endregion

    }
}
