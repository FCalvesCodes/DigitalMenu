using DigitalMenu.Application.Common.Models;

namespace DigitalMenu.Application.Core.Applications
{
    public interface ICrudAppService : IDisposable
    {
    }

    public interface ICrudAppService<TId, TEntityDto> : ICrudAppService
        where TId : struct
        where TEntityDto : class, IEntityDto<TId>
    {
        Task<TEntityDto> GetAsync(TId id);
        Task<IList<TEntityDto>> GetAllAsync();
        Task<TEntityDto> CreateAsync(TEntityDto dto);
        Task<TEntityDto> UpdateAsync(TEntityDto dto);
        Task DeleteAsync(TId id);
    }
}
