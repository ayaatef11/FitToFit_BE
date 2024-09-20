using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace Modules.Identity.Data.EntityModels
{
    public class ApplicationUser : IdentityUser, IAuditable
    {
        private ApplicationUser()
        {
            IsDeleted = false;
            IsActive = true;
        }
        public UserTypeInfo UserTypeInfo { get; private set; }
        public byte UserTypeInfoId { get; private set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; private set; }
        public string NationalId { get; private set; }
        public string FullNameEn { get; private set; }
        public string FullNameAr { get; private set; }
        public string BirthDate { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastLoginDate { get; private set; }

        public void Activate()
            => IsActive = true;
        public void DeActivate()
           => IsActive = false;

        public void Delete()
        {
            IsDeleted = true;
            UserRoles = new Collection<ApplicationUserRole>();
        }

        internal void RevertDelete()
        {
            IsDeleted = false;
        }
    }

}
