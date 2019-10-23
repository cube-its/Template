using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CompanyName.ProjectName.API.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using CompanyName.ProjectName.Common.Exceptions;
using CompanyName.ProjectName.Model;

namespace CompanyName.ProjectName.API.AspCoreExtensions
{
    /// <summary>
    /// Middleware for delivering a response to unhandled exceptions.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        //private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)//, ILoggerManager logger)
        {
            _next = next;
            _env = env;
            //_logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            int statusCode;
            ErrorCode errorCode;
            object data = null;
            bool moreDetails = _env.IsDevelopment();

            var errors = new string[] { error.Message };

            if (error is NotFoundException || error is KeyNotFoundException || error is ArgumentOutOfRangeException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                errorCode = ErrorCode.NotFound;
            }
            else if (error is ArgumentException || error is InvalidOperationException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorCode = ErrorCode.BadRequest;
            }
            else if (error is NotUniqueEmailException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorCode = ErrorCode.NotUniqueEmail;

                moreDetails = false;
            }
            else if (error is NotUniquePhoneException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorCode = ErrorCode.NotUniquePhone;

                moreDetails = false;
            }
            else if (error is InvalidPhoneOrEmailException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorCode = ErrorCode.InvalidPhoneOrEmail;

                moreDetails = false;
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                errorCode = ErrorCode.InternalServerError;
            }

            if (moreDetails)
            {
                data = new { error.StackTrace, error.Data };
            }

            context.Response.ContentType = WebserviceCommon.JSON_MIME;
            context.Response.StatusCode = statusCode;

            var message = new ErrorResponse((int)errorCode, errorCode.ToString(), errors, data);
            var result = JsonConvert.SerializeObject(message);

            return context.Response.WriteAsync(result);
        }
    }
}
