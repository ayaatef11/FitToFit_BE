using MediatR;

namespace SharedKernal.BuildingBlocks
{
    public interface IDomainEvent : INotification
    {
        public string EventType => GetType().AssemblyQualifiedName ?? "<Unknown Domain Event Type>";
    }
}
