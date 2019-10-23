using CompanyName.ProjectName.Domain.Contracts.Services;
using CompanyName.ProjectName.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CompanyName.ProjectName.Domain
{
    public class SecurityService : ISecurityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecurityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetAuthenticatedUser()
        {
            User user = new User();
            user.Id = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value);
            user.Email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            user.Phone = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "phone").Value;

            return user;
        }
    }
}
