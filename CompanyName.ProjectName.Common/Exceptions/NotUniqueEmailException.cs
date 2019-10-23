using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ProjectName.Common.Exceptions
{
    public class NotUniqueEmailException : Exception
    {
        private const string Message = "Email address is already registered!";

        public NotUniqueEmailException()
            : base(Message)
        {
        }
    }
}
