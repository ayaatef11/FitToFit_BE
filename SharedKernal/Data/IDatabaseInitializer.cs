namespace SharedKernal.Data
{
    public interface IDatabaseInitializer
    {
        Task SeedAndCheckDefaultData(CancellationToken token = default);
    }
}
