using Jobsity.Chat.Domain.Interfaces.Repositories.Base;
using Jobsity.Chat.Domain.Interfaces.Services.Base;
using Jobsity.Chat.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.Domain.Services.Base
{
    public abstract class BaseService<TModel, TRepository> : IBaseService<TModel> 
        where TModel : BaseModel
        where TRepository : IBaseRepository<TModel>
    {

        protected readonly TRepository _repository;

        protected BaseService(TRepository repository)
        {
            _repository = repository;
        }

        public async Task<TModel> Create(TModel model)
        {
            return await _repository.Create(model);
        }

        public Task Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public Task<IEnumerable<TModel>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<TModel> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<TModel> Update(TModel model)
        {
            return _repository.Update(model);
        }
    }
}
