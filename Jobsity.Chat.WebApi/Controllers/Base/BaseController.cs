using Jobsity.Chat.Application.Interfaces.Base;
using Jobsity.Chat.Application.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.WebApi.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TViewModel, TAppService> : ControllerBase
        where TViewModel : BaseViewModel
        where TAppService : IBaseAppService<TViewModel>
    {
        protected readonly TAppService _appService;

        public BaseController(TAppService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        public Task<BaseResponse<TViewModel>> Create(TViewModel model)
        {
            return _appService.Create(model);
        }

        [HttpDelete]
        public Task<BaseResponse<object>> Delete(Guid id)
        {
            return _appService.Delete(id);
        }

        [HttpGet]
        public Task<BaseResponse<IEnumerable<TViewModel>>> GetAll()
        {
            return _appService.GetAll();
        }

        [HttpGet("{id}")]
        public Task<BaseResponse<TViewModel>> GetByIdAsync(Guid id)
        {
            return _appService.GetByIdAsync(id);
        }

        [HttpPut]
        public Task<BaseResponse<TViewModel>> Update(TViewModel model)
        {
            return _appService.Update(model);
        }
    }
}
