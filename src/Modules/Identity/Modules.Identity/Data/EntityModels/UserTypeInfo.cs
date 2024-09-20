using Modules.Identity.Data.Enums;

namespace Modules.Identity.Data.EntityModels
{
    public sealed class UserTypeInfo
    {
        private UserTypeInfo()
        {

        }
        public byte Id { get; private set; }
        public string NameEn { get; private set; }
        public string NameAr { get; private set; }

        public bool IsDoctor => UserType.Doctor == Id;
        public bool IsClient => UserType.Client == Id;

        public static UserTypeInfo Create(byte id)
        {
            var obj = new UserTypeInfo
            {
                Id = id
            };

            //these are default values , can be changed by the admin
            if (obj.IsDoctor)
            {
                obj.NameEn = "doctor";
                obj.NameAr = "طبيب";
            }

            if (obj.IsClient)
            {
                obj.NameEn = "client";
                obj.NameAr = "عميل";
            }

            return obj;
        }
    }
}
