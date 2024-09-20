using SharedKernal.Data;

namespace Modules.Doctors.Data
{
    internal sealed class DoctorsDatabaseInitializer(DoctorsDbContext db) : IDatabaseInitializer
    {
        public Task SeedAndCheckDefaultData(CancellationToken token = default)
        {
            return Task.CompletedTask;
        }
    }
}
