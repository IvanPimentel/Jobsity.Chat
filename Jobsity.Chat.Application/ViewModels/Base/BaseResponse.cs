using System;

namespace Jobsity.Chat.Application.ViewModels.Base
{
    public class BaseResponse<TData>
    {
        public bool Success { get; private set; } = true;
        public string Message { get; private set; }
        public TData Data { get; set; }

        public BaseResponse(TData data)
        {
            Data = data;
            Success = true;
        }

        public BaseResponse(Exception ex)
        {
            Success = false;
            Message = ex.Message;
        }
    }
}
