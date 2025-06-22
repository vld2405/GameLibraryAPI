using GameLibrary.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("request1");
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await RespondToExceptionAsync(context, HttpStatusCode.NotFound, ex.Message, ex);
            }
            catch (Exception ex)
            {
                await RespondToExceptionAsync(context, HttpStatusCode.InternalServerError, "Internal Server Error", ex);
            }

            Console.WriteLine("response1");
            Console.WriteLine();
        }

        private static Task RespondToExceptionAsync(HttpContext context, HttpStatusCode failureStatusCode, string message, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)failureStatusCode;

            var response = new
            {
                Message = message,
                Error = exception.GetType().Name,
                Timestamp = DateTime.UtcNow
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }));
        }
    }
}
