using Day27_Webapi_EF.DTO;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Day27_Webapi_EF.Misc
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(new ErrorResponseDTO
            {
                ErrorMessage = context.Exception.Message,
                ErrorNumber = 500
            });
        }
    }
}
