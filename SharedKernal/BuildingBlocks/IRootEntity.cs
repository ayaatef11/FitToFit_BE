namespace SharedKernal.BuildingBlocks
{
    public interface IRootEntity
    {
        void AddEvent(IDomainEvent domainEvent);
        void AddEvents(IEnumerable<IDomainEvent> domainEvents);
        void ClearDomainEvents();
        bool IsEventAdded<TEvent>() where TEvent : IDomainEvent;
        bool IsEventAdded(IDomainEvent domainEvent);
        IEnumerable<IDomainEvent> UncommittedEvents { get; }
    }
}
