using SharedKernal.EnumsAbstraction;

namespace Modules.Identity.Data.Enums
{
    public sealed record UserType : SmartEnum<UserType, byte>
    {
        public UserType(byte Key) : base(Key)
        {
        }

        public readonly static UserType Doctor = new(1);
        public readonly static UserType Client = new(2);

        public bool IsDoctor => Key == Doctor.Key;
        public bool IsClient => Key == Client.Key;
    }
}
