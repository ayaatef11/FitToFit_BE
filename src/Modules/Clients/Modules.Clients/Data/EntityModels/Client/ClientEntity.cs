using SharedKernal.BuildingBlocks;

namespace Modules.Clients.Data.EntityModels.Client
{
    public sealed class ClientEntity : RootEntity<int>
    {
        public string NameAr { get; set; }=string.Empty;
        public string NameEn { get; set; }=string.Empty;
        public int Age { get; set; }
        public int DoctorId {  get; set; }  
    }
}
