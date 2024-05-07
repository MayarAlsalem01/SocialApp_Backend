using Microsoft.AspNetCore.Mvc;
using SocialApp.Domain.Exceptions.Common;
using SocialApp.Domain.Exceptions.PostExceptions;
using SocialApp.Domain.Exceptions.RoleExceptions;
using SocialApp.Domain.Exceptions.UserExceptions;
using SocialApp.Dtos.Response;
using System.Net;

namespace SocialApp.Middleware.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occurred.");

            //More log stuff        
           
            ExceptionResponse response = exception switch
            {

             
                NotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, exception.Message),
                AlreadyExistException _ => new ExceptionResponse(HttpStatusCode.BadRequest, exception.Message),
                ForbiddenException _ => new ExceptionResponse(HttpStatusCode.Forbidden, exception.Message),
                UnauthorizedException _ => new ExceptionResponse(HttpStatusCode.Unauthorized, exception.Message),
                UserBadRequestException _=>new ExceptionResponse(HttpStatusCode.BadRequest,exception.Message,((UserBadRequestException)exception).errors),
                _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.\n "+exception.Message+exception.InnerException)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;            
            var pro = new ProblemDetails();
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
