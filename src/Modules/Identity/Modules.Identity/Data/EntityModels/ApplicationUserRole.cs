using Microsoft.AspNetCore.Identity;

namespace Modules.Identity.Data.EntityModels
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationRole Role { get; private set; }
    }
}
