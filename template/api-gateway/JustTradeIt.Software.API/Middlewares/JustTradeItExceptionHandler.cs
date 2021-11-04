using System;
using System.Net;
using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.Exceptions;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;


namespace JustTradeIt.Software.API.Middlewares
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            // TODO: Implement
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    // Retrieve the exception handler feature
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        var exception = exceptionHandlerFeature.Error;
                        var statusCode = (int) HttpStatusCode.InternalServerError;
                        //If else
                        if (exception is ArgumentOutOfRangeException)
                        {
                            statusCode = (int) HttpStatusCode.BadRequest;
                        }
                        else if (exception is ResourceNotFoundException)
                        {
                            statusCode = (int) HttpStatusCode.NotFound;
                        }
                        else if (exception is ModelFormatException)
                        {
                            statusCode = (int) HttpStatusCode.PreconditionFailed;
                        }
                        else if (exception is CannotCreateTradeException)
                        {
                            statusCode = (int) HttpStatusCode.BadRequest;
                        }
                        else if (exception is CannotDeleteItemException)
                        {
                            statusCode = (int) HttpStatusCode.BadRequest;
                        }
                        else if (exception is CannotUpdateTradeException)
                        {
                            statusCode = (int) HttpStatusCode.BadRequest;
                        }
                        else if (exception is ResourceAlreadyExistsException)
                        {
                            statusCode = (int) HttpStatusCode.BadRequest;
                        }

                        context.Response.StatusCode = statusCode;
                        context.Response.ContentType = "application/json";
                        
                        await context.Response.WriteAsync(new ExceptionModel
                        {
                            StatusCode = statusCode,
                            ExceptionMessage = exception.Message
                        
                        }.ToString());
                        
                        
                    }
                    
                });
                
                
            });


        }
    }
}