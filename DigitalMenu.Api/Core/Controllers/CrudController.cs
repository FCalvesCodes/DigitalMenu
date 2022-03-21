using DigitalMenu.Application.Common.Models;
using DigitalMenu.Application.Core.Applications;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMenu.Api.Core.Controllers
{
    public abstract class CrudBase : ControllerBase
    {
    }

    public abstract class CrudController<TId, TEntityDto, TAppService> : CrudBase
        where TId : struct
        where TEntityDto : class, IEntityDto<TId>
        where TAppService : class, ICrudAppService<TId, TEntityDto>
    {
        protected readonly TAppService appService;

        public CrudController(TAppService appService)
        {
            this.appService = appService;
        }

        public virtual async Task<IActionResult> GetAllAsync()
        {
            return Ok(await appService.GetAllAsync());
        }

        public virtual async Task<IActionResult> GetAsync(TId id)
        {
            return Ok(await appService.GetAsync(id));
        }

        public virtual async Task<IActionResult> CreateAsync([FromBody] TEntityDto dto)
        {
            return Ok(await appService.CreateAsync(dto));
        }

        public virtual async Task<IActionResult> UpdateAsync(TId id, [FromBody] TEntityDto dto)
        {
            dto.Id = id;
            return Ok(await appService.UpdateAsync(dto));
        }

        public virtual async Task<IActionResult> DeleteAsync(TId id)
        {
            await appService.DeleteAsync(id);
            return Ok();
        }
    }
}
