using CompanyName.ProjectName.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Domain.Contracts.Services
{
    public interface IJwtFactory
    {
        /// <summary>
        /// Generates access token using the user information.
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>Access token</returns>
        Task<Token> GenerateEncodedToken(User user);
    }
}
