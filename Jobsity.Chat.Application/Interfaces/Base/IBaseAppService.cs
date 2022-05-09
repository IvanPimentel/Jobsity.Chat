using Jobsity.Chat.Application.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Interfaces.Base
{
    public interface IBaseAppService<TViewModel> where TViewModel : BaseViewModel
    {
        Task<BaseResponse<IEnumerable<TViewModel>>> GetAll();
        Task<BaseResponse<TViewModel>> GetByIdAsync(Guid id);
        Task<BaseResponse<TViewModel>> Create(TViewModel model);
        Task<BaseResponse<TViewModel>> Update(TViewModel model);
        Task<BaseResponse<object>> Delete(Guid id);
    }
}
