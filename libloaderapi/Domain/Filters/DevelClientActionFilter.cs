using System;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;
using libloaderapi.Domain.Attributes;
using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace libloaderapi.Domain.Filters
{
    public class DevelClientActionFilter : IAsyncActionFilter
    {
        private readonly IClientsService _clientsService;

        public DevelClientActionFilter(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;

            // We're only interested in users with the client role
            if (!user.IsInRole(PredefinedRoles.Client))
            {
                await next();
                return;
            }

            // Skip when we don't have a guid
            if (!Guid.TryParse(user.Identity.Name, out var clientId))
            {
                await next();
                return;
            }

            if (context.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                // Check if the target controller or method has been marked with the DevelClientRestrictedAttribute
                if (descriptor.MethodInfo.GetCustomAttributes<DevelClientRestrictedAttribute>().Any() ||
                    context.Controller.GetType().GetCustomAttributes<DevelClientRestrictedAttribute>().Any())
                {
                    var client = await _clientsService.GetByClientIdAsync(clientId);
                    if (client == null)
                    {
                        // Client does not exist anymore
                        context.Result = new NotFoundObjectResult("This client does not exist anymore.\n");
                        return;
                    }

                    if (client.RegistrantIp != GetIpAddress(context.HttpContext))
                    {
                        context.Result = new BadRequestObjectResult("Seems like you're trying to use your development access for production. " +
                                                                    "Please re-upload your client to the Production bucket.\n");
                        return;
                    }

                    // Devel restrictions doesn't apply, continue.
                    await next();
                }
            }
            else
            {
                // Something's gone really wrong here
                throw new ApplicationException();
            }
        }

        private static string GetIpAddress(HttpContext context)
        {
            return context.Request.Headers.TryGetValue("X-Real-IP", out var ipAddress)
                ? ipAddress.ToString()
                : context.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
