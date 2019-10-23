using CompanyName.ProjectName.Common.Settings;
using System;

namespace CompanyName.ProjectName.Integration
{
    /// <summary>
    /// Interface that defines the integration with Cube system resources.
    /// </summary>
    public interface ICubeResourceManager
    {
        /// <summary>
        /// Checks if the given phone number already exists in Cube database.
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <returns>Boolean</returns>
        bool PhoneExists(string phone);

        /// <summary>
        /// Sends SMS verification code to the given phone number.
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <returns>Verification code</returns>
        string SendSmsVerificationCode(string phone);
    }
}
