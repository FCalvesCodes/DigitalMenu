using AutoMapper;
using DigitalMenu.Application.Common.Models;
using DigitalMenu.Domain.Core.Entities;
using DigitalMenu.Domain.Core.Repositories;

namespace DigitalMenu.Application.Core.Applications
{
    public abstract class CrudAppService : ICrudAppService
    {
        public virtual void Dispose()
        { }
    }

    public abstract class CrudAppService<TEntity, TId, TEntityDto, TRepository> : CrudAppService, ICrudAppService<TId, TEntityDto>
        where TId : struct
        where TEntity : class, IEntity<TId>
        where TEntityDto : class, IEntityDto<TId>
        where TRepository : class, IRepository<TEntity, TId>
    {
        private readonly IMapper mapper;
        protected readonly TRepository repository;

        public CrudAppService(IMapper mapper, TRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<TEntityDto> CreateAsync(TEntityDto dto)
        {
            var entity = mapper.Map<TEntityDto, TEntity>(dto);
            await repository.CreateAsync(entity);
            return mapper.Map<TEntityDto>(entity);
        }

        public Task DeleteAsync(TId id)
        {
            return repository.DeleteAsync(id);
        }

        public async Task<IList<TEntityDto>> GetAllAsync()
        {
            return (await repository.GetAllAsync()).Select(c => mapper.Map<TEntity, TEntityDto>(c)).ToList();
        }

        public async Task<TEntityDto> GetAsync(TId id)
        {
            return mapper.Map<TEntityDto>(await repository.GetAsync(id));
        }

        public async Task<TEntityDto> UpdateAsync(TEntityDto dto)
        {
            var existingEntity = await repository.GetAsync(dto.Id);
            var entity = mapper.Map(dto, existingEntity);
            await repository.UpdateAsync(entity);
            return mapper.Map<TEntityDto>(entity);
        }
    }
}
