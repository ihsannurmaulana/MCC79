using API.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Utilities
{
    public class CreateResponseHandler<TDto> : ControllerBase
    {
        public static ResponseHandler<TDto> createResponse(int code, string status, string message, TDto data)
        {
            return new ResponseHandler<TDto>
            {
                Code = code,
                Status = status,
                Message = message,
                Data = data
            };
        }
    }
}
