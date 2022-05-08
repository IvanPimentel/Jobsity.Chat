using AutoMapper;
using Jobsity.Chat.Application.Interfaces.Base;
using Jobsity.Chat.Application.ViewModels.Base;
using Jobsity.Chat.Domain.Interfaces.Services.Base;
using Jobsity.Chat.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Services.Base
{
    public abstract class BaseAppService<TViewModel, TModel, TService> : IBaseAppService<TViewModel>
        where TViewModel : BaseViewModel
        where TModel : BaseModel
        where TService : IBaseService<TModel>
    {
        protected readonly TService _service;
        protected readonly IMapper _mapper;

        protected BaseAppService(TService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<BaseResponse<TViewModel>> Create(TViewModel model)
        {
            try
            {
                var serviceResult = await _service.Create(_mapper.Map<TModel>(model));
                return new BaseResponse<TViewModel>(_mapper.Map<TViewModel>(serviceResult));
            }
            catch (Exception ex)
            {
                return new BaseResponse<TViewModel>(ex);
            }
        }

        public async Task<BaseResponse<object>> Delete(Guid id)
        {
            try
            {
                await _service.Delete(id);
                return new BaseResponse<object>(null);
            }
            catch (Exception ex)
            {
                return new BaseResponse<object>(ex);
            }
        }

        public async Task<BaseResponse<IEnumerable<TViewModel>>> GetAll()
        {
            try
            {
                var serviceResult = await _service.GetAll();
                return new BaseResponse<IEnumerable<TViewModel>>(_mapper.Map<IEnumerable<TViewModel>>(serviceResult));
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TViewModel>>(ex);
            }
        }

        public async Task<BaseResponse<TViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var serviceResult = await _service.GetByIdAsync(id);
                return new BaseResponse<TViewModel>(_mapper.Map<TViewModel>(serviceResult));
            }
            catch (Exception ex)
            {
                return new BaseResponse<TViewModel>(ex);
            }
        }

        public async Task<BaseResponse<TViewModel>> Update(TViewModel model)
        {
            try
            {
                var serviceResult = await _service.Update(_mapper.Map<TModel>(model));
                return new BaseResponse<TViewModel>(_mapper.Map<TViewModel>(serviceResult));
            }
            catch (Exception ex)
            {
                return new BaseResponse<TViewModel>(ex);
            }
        }
    }
}
