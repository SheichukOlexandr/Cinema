using AutoMapper;
using DataAccess.Repositories.Interfaces;
using System.Net;

namespace BusinessLogic.Services
{
    public abstract class BaseService<TDto, TEntity>
        where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        protected BaseService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task AddAsync(TDto dto)
        {
            await _repository.AddAsync(_mapper.Map<TEntity>(dto));
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task UpdateAsync(TDto dto)
        {
            await _repository.UpdateAsync(_mapper.Map<TEntity>(dto));
        }

        public virtual async Task<TDto> GetAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("" + HttpStatusCode.NotFound);

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }
    }
}
