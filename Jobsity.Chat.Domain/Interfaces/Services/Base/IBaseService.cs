using Jobsity.Chat.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.Domain.Interfaces.Services.Base
{
    public interface IBaseService<TModel> where TModel : BaseModel
    {
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel> GetByIdAsync(Guid id);
        Task<TModel> Create(TModel model);
        Task<TModel> Update(TModel model);
        Task Delete(Guid id);
    }
}
