using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Day29_BackendPlan.DTO;

namespace Day29_BackendPlan.Misc
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
