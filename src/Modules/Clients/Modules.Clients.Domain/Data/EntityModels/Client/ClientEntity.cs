using SharedKernal.BuildingBlocks;

namespace Modules.Clients.Data.EntityModels.Client
{
    public sealed class ClientEntity : RootEntity<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int Age { get; set; }
    }
}
