using CompanyName.ProjectName.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.API.AspCoreExtensions
{
    /// <summary>
    /// Implements automatically sending an HTTP 400 (bad request) response when the model state is invalid.
    /// </summary>
    internal class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage);
                context.Result = new BadRequestObjectResult(new ErrorResponse((int)ErrorCode.BadRequest, ErrorCode.BadRequest.ToString(), errors));
            }
        }
    }
}
