using Microsoft.AspNetCore.Identity;

namespace Modules.Identity.Data.EntityModels
{
    public class ApplicationRole : IdentityRole, IAuditable
    {
        private ApplicationRole()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }


        public static ApplicationRole Create(string name)
        {
            return new ApplicationRole
            {
                Name = name
            };
        }
    }

}
