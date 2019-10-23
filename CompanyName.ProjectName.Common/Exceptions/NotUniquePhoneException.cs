using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ProjectName.Common.Exceptions
{
    public class NotUniquePhoneException : Exception
    {
        private const string Message = "Phone number is already registered!";

        public NotUniquePhoneException()
            : base(Message)
        {
        }
    }
}
