using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Errors;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Api.Middleware
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _evn;
    
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment evn)
        {
            _evn = evn;
            _logger = logger;
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch(Exception ex)
            {
            _logger.LogError( ex, ex.Message);
            context.Response.ContentType="application/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

            var response = _evn.IsDevelopment() 
            ? new ApiException(context.Response.StatusCode, ex.Message,ex.StackTrace?.ToString()) 
            : new ApiException(context.Response.StatusCode, ex.Message,"Internal server Error");

            var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            var Json = JsonSerializer.Serialize(response , options);
            await context.Response.WriteAsync(Json);
            }
        }

    }
}