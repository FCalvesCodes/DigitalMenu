using DigitalMenu.Application.Core.WorkUnits;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigitalMenu.Api.Core.WorkUnits
{
    public class UnitOfWorkActionFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider serviceProvider;

        public UnitOfWorkActionFilter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor))
            {
                await next();
                return;
            }

            using (var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>())
            {
                var result = await next();

                if (result.Exception == null || result.ExceptionHandled)
                {
                    await unitOfWork.CompleteAsync();
                }
            }
        }
    }
}
