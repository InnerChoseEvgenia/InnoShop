using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using User.Application.Dto;

namespace User.API.ExceptionHandler

{
    public  class GlobalExceptionHandler

    {

        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandler(RequestDelegate next, 
            ILogger<GlobalExceptionHandler> logger, 
            IHostEnvironment env)
        {
            _next=next;
            _logger=logger;
            _env=env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // context.Response
                //     .HttpContext
                //     .Features
                //     .Get<IHttpResponseFeature>()
                //     .ReasonPhrase = ex.Message;

                var response = _env.IsDevelopment()
                    ? new ErrorDetails(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ErrorDetails(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);

            }
        }


    }
}

        //public async Task InvokeAsync (HttpContext context)
        //{
        //    try
        //    {
        //        await _next(context);
        //    }
        //    catch(UnauthorizedAccessException ex)
        //    {
        //        await HandleExceptionAsync(
        //            context, 
        //            ex.Message,
        //            HttpStatusCode.Unauthorized,
        //           "Invalid username or password");
        //    }
        //    catch (BadHttpRequestException ex)
        //    {
        //        await HandleExceptionAsync(
        //            context,
        //            ex.Message,
        //            HttpStatusCode.BadRequest,
        //           "Failed to send email. Please contact admin");
        //    }
        //    catch (Exception ex)
        //    {
        //        await HandleExceptionAsync(
        //            context,
        //            ex.Message,
        //            HttpStatusCode.InternalServerError,
        //           "Internal server error");
        //    }
        //}

        //private async Task HandleExceptionAsync(HttpContext context, string exMsg, HttpStatusCode httpStatusCode, string message )
        //{
        //    _logger.LogError(exMsg);
        //    HttpResponse response = context.Response;
        //    response.ContentType = "application/json";
        //    response.StatusCode = (int)httpStatusCode;

        //    ErrorDetails details = new()
        //    {
        //        Message = message,
        //        StatusCode = (int)httpStatusCode
        //    };

        //    string result = JsonSerializer.Serialize(details);

        //    await response.WriteAsJsonAsync(result);
        //}


        ////public static void ConfigureExceptionHandler(this WebApplication app)
        ////{
        ////    app.UseExceptionHandler(appError =>
        ////    {
        ////        appError.Run(async context =>
        ////        {
        ////            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        ////            context.Response.ContentType = "application/json";
        ////            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        ////            if (contextFeature != null)
        ////            {
        ////                await context.Response.WriteAsync(new ErrorDetails()
        ////                {
        ////                    StatusCode = context.Response.StatusCode,
        ////                    Message = "Internal Server Error.",
        ////                }.ToString());
        ////            }
        ////        });
        ////    });
        ////}
//    }
//}
   
