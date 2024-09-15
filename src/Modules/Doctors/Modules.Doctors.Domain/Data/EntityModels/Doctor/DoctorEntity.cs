using SharedKernal.BuildingBlocks;

namespace Modules.Doctors.Data.EntityModels.Doctor
{
    public sealed class DoctorEntity : RootEntity<int>
    {
        public string DrNameAr { get; set; }
        public string DrNameEn { get; set; }
        public int Age { get; set; }
    }
}
