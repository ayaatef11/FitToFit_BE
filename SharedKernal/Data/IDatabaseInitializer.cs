namespace SharedKernal.Data
{
    public interface IDatabaseInitializer
    {
        //here we initalize data for all modules 
        Task SeedAndCheckDefaultData(CancellationToken token = default);
    }
}
