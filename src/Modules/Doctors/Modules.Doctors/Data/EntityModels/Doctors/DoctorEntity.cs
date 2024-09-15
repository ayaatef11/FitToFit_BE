using SharedKernal.BuildingBlocks;

namespace Modules.Doctors.Domain.EntityModels.Doctors
{
    public sealed class DoctorEntity : Entity<DoctorEntity, int>
    {
        public string NameEn { get; private set; }
        public string NameAr { get; private set; }
        public string Address { get; private set; }
        private DoctorEntity()
        {

        }
        public static DoctorEntity Create(string nameEn, string nameAr, string address)
        {
            return new DoctorEntity
            {
                Address = address,
                NameEn = nameEn,
                NameAr = nameAr
            };
        }
    }
}
