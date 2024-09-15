using Microsoft.AspNetCore.Authorization;

namespace SharedKernal.Policies
{
    public static class PolicyNames
    {
        public const string HasClientPermissions = nameof(HasClientPermissions);
        public const string HasDoctorsPermissions = nameof(HasDoctorsPermissions);
    }
    public static class PolicyMangement
    {
        public static void AddSystemPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(PolicyNames.HasDoctorsPermissions, policy =>
            {
                policy.RequireAuthenticatedUser()
                    .RequireAssertion(ctx =>
                    {
                        return ctx.User.Identity.IsAuthenticated;
                    });
            });

            options.AddPolicy(PolicyNames.HasClientPermissions, policy =>
            {
                policy.RequireAuthenticatedUser()
                    .RequireAssertion(ctx =>
                    {
                        return ctx.User.Identity.IsAuthenticated;
                    });
            });
        }
    }
}
