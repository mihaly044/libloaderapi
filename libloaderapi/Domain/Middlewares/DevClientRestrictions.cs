using System;
using System.Threading.Tasks;
using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace libloaderapi.Domain.Middlewares
{
    public class DevClientRestrictions
    {
        private readonly RequestDelegate _next;

        public DevClientRestrictions(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IClientsService clientsService)
        {
            if (!context.User.IsInRole(PredefinedRoles.Client))
            {
                await _next(context);
                return;
            }

            if(!Guid.TryParse(context.User.Identity.Name, out var clientId))
            {
                await _next(context);
                return;
            }

            var client = await clientsService.GetByClientIdAsync(clientId);
            if (client == null)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            if (client.RegistrantIp != GetIpAddress(context))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }

        private static string GetIpAddress(HttpContext context)
        {
            return context.Request.Headers.TryGetValue("X-Real-IP", out var ipAddress)
                ? ipAddress.ToString()
                : context.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }

    public static class DevClientRestrictionsEx
    {
        public static IApplicationBuilder UseDevClientRestrictions (this IApplicationBuilder builder) 
            => builder.UseMiddleware<DevClientRestrictions>();
    }
}
