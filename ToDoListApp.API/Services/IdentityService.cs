
using Microsoft.AspNetCore.Http;
using System;

namespace ToDoListApp.API.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetCustomerIdentity()
        {
            return _context.HttpContext.User.FindFirst("sub").Value;
        }

        public string GetCustomerName()
        {
            return _context.HttpContext.User.Identity.Name;
        }
    }
}
