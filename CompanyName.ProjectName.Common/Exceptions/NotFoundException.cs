using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ProjectName.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        private const string Message = "Not Found";

        public NotFoundException()
            : base(Message)
        {
        }
    }
}
