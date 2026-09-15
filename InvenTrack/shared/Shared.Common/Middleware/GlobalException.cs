using Microsoft.AspNetCore.Http;
using Shared.Common.Logs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Shared.Common.Exceptions;

namespace Shared.Common.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            string message = "sorry, internal server error occurred. Kindly try again";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Error";

            try
            {
                await next(context);

                // If downstream set a non-success status, format the response
                if (context.Response.HasStarted)
                    return;

                switch (context.Response.StatusCode)
                {
                    case StatusCodes.Status404NotFound:
                        message = "The requested resource was not found.";
                        statusCode = (int)HttpStatusCode.NotFound;
                        title = "Not Found";
                        await ModifyHeader(context, title, message, statusCode, context.TraceIdentifier);
                        break;
                    case StatusCodes.Status400BadRequest:
                        message = "The request was invalid or cannot be served.";
                        statusCode = (int)HttpStatusCode.BadRequest;
                        title = "Bad Request";
                        await ModifyHeader(context, title, message, statusCode, context.TraceIdentifier);
                        break;
                    case StatusCodes.Status401Unauthorized:
                        message = "You are not authorized to access this resource.";
                        statusCode = (int)HttpStatusCode.Unauthorized;
                        title = "Unauthorized";
                        await ModifyHeader(context, title, message, statusCode, context.TraceIdentifier);
                        break;
                    case StatusCodes.Status429TooManyRequests:
                        message = "You have sent too many requests in a given amount of time.";
                        statusCode = (int)HttpStatusCode.TooManyRequests;
                        title = "Too Many Requests";
                        await ModifyHeader(context, title, message, statusCode, context.TraceIdentifier);
                        break;
                    case StatusCodes.Status403Forbidden:
                        message = "You do not have permission to access this resource.";
                        statusCode = (int)HttpStatusCode.Forbidden;
                        title = "Forbidden";
                        await ModifyHeader(context, title, message, statusCode, context.TraceIdentifier);
                        break;
                }
            }
            catch (Exception ex)
            {
                // derive correlation id from header if provided, otherwise use trace id
                var correlationId = context.Request.Headers.TryGetValue("X-Correlation-ID", out var hdr) && !string.IsNullOrWhiteSpace(hdr) ? hdr.ToString() : context.TraceIdentifier;

                // centralized logging with correlation id
                LogException.LogExceptions(ex, correlationId);

                if (ex is TaskCanceledException || ex is TimeoutException)
                {
                    message = "The request timed out. Please try again later.";
                    statusCode = (int)HttpStatusCode.RequestTimeout;
                    title = "Request Timeout";
                }
                else if (ex is ValidationException)
                {
                    message = ex.Message;
                    statusCode = (int)HttpStatusCode.BadRequest;
                    title = "Bad Request";
                }
                else if (ex is NotFoundException)
                {
                    message = ex.Message;
                    statusCode = (int)HttpStatusCode.NotFound;
                    title = "Not Found";
                }

                await ModifyHeader(context, title, message, statusCode, correlationId);
            }
        }

        private async Task ModifyHeader(HttpContext context, string title, string message, int statusCode, string correlationId)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var response = new
            {
                Title = title,
                Detail = message,
                Status = statusCode,
                CorrelationId = correlationId
            };
            // include correlation id header as well
            if (!context.Response.Headers.ContainsKey("X-Correlation-ID"))
                context.Response.Headers.Add("X-Correlation-ID", correlationId);

            await context.Response.WriteAsJsonAsync(response, CancellationToken.None);
            return;
        }
    }
}
