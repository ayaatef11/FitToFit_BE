using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.UserPrincipals
{
    public static class UserPrincipalExtension
    {
        //User is defined in the claim principal
        public static int GetId(this ClaimsPrincipal userPrincipal)
        {
            return int .Parse(userPrincipal.FindFirst("ClientId").Value);
        }
    }
}
