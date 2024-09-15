namespace SharedKernal.BuildingBlocks
{
    public interface IEntity<TId>
    {
        public TId Id { get; set; }
    }
}
