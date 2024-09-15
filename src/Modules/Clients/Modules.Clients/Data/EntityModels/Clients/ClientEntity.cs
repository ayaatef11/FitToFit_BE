using SharedKernal.BuildingBlocks;

namespace Modules.Clients.Domain.EntityModels.Clients
{
    public sealed class ClientEntity : Entity<ClientEntity, int>
    {
        public string NameEn { get; private set; }
        public string NameAr { get; private set; }
        public string Address { get; private set; }
        private ClientEntity()
        {

        }
        public static ClientEntity Create(string nameEn, string nameAr, string address)
        {
            return new ClientEntity
            {
                Address = address,
                NameEn = nameEn,
                NameAr = nameAr
            };
        }
    }
}
