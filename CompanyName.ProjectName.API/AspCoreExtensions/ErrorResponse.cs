using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.API.AspCoreExtensions
{
    /// <summary>
    /// Encapsulates error messages and optional data to send as an API response.
    /// </summary>
    public class ErrorResponse
    {
        public int Code { get; }
        public string Name { get; }
        public List<string> Errors { get; }
        public object Data { get; }

        /// <summary>
        /// Create an instance from a single error message and some optional data.
        /// </summary>
        public ErrorResponse(int code, string name, string error, object data = null)
        {
            if (error == null)
                throw new ArgumentNullException(nameof(error));

            Code = code;
            Name = name;
            Errors = new List<string> { error };
            Data = data;
        }

        /// <summary>
        /// Create an instance from multiple error messages and some optional data.
        /// </summary>
        public ErrorResponse(int code, string name, IEnumerable<string> errors, object data = null)
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));

            Code = code;
            Name = name;
            Errors = new List<string>(errors);
            Data = data;
        }
    }
}
