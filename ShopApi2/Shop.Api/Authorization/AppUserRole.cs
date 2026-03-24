using Microsoft.AspNetCore.Authorization;
using Shop.Models;
using Shop.Services.Interfaces;
using System.Security.Claims;

namespace Shop.Api.Authorization
{
    public class AppUserRole : AuthorizationHandler<AppUserRoleRequirement, Customer>
    {
        private readonly ICustomerService _customerService;

        public AppUserRole(ICustomerService projectService) => _customerService = projectService;

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AppUserRoleRequirement requirement,
            Customer resource)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return;

            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return;
            }

            if (context.User.IsInRole("Manager"))
            {
                context.Succeed(requirement);
                return;
            }

        
        }
    
    }
}
