using CompanyName.ProjectName.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ProjectName.Domain.Contracts.Services
{
    /// <summary>
    /// User service interface that exposes the relevant operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>List of users</returns>
        IEnumerable<User> GetUsers();

        /// <summary>
        /// Gets the user by phone number.
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <returns>User object</returns>
        User GetByPhone(string phone);

        /// <summary>
        /// Authenticates the user using phone number and email address.
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <param name="email">Email address</param>
        /// <returns>User object</returns>
        User Login(string phone, string email);

        /// <summary>
        /// Registers the user using phone number and email address.
        /// </summary>
        /// <param name="obj">Register object containing phone and email</param>
        User Register(User obj);

        /// <summary>
        /// Checks if phone number is not used.
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <returns>Boolean</returns>
        bool IsPhoneUnique(string phone);

        /// <summary>
        /// Checks if email address is not used.
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>Boolean</returns>
        bool IsEmailUnique(string email);


        /// <summary>
        /// Checks if the verification code is valid for the given phone number.
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <param name="code">Verification code to be checked</param>
        /// <returns>Boolean</returns>
        bool VerifyPhoneCode(string phone, string code);

        /// <summary>
        /// Sends SMS verification code to the given phone number.
        /// </summary>
        /// <param name="phone">Phone number</param>
        void SendSmsVerificationCode(string phone);
    }
}
