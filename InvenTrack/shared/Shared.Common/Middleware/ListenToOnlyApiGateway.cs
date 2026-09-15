using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common.Middleware
{
    public class ListenToOnlyApiGateway(RequestDelegate next)
    {

        public async Task InvokeAsync(HttpContext context)
        {

            var signedHeader = context.Request.Headers["Api-Gateway"];
            if (signedHeader.FirstOrDefault() is null)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Service Unavailable: Missing Api-Gateway header.");
                return;
            }
            else
            {
                await next(context);
            }
           
        }
    }
}
