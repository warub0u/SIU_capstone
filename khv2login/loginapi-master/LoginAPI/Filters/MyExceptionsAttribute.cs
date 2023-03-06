using LoginAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LoginAPI.Filters
{
    public class MyExceptionsAttribute: ExceptionFilterAttribute
    {
        // EXCEPTION HANDLING BASED ON EXCEPTION TYPES
        public override void OnException(ExceptionContext context)
        {
            var message = context.Exception.Message;
            var exceptionType = context.Exception.GetType();


            if ((exceptionType == typeof(UserNotFoundException)))
            {
                context.Result = new NotFoundObjectResult(message);
            }
            else if ((exceptionType == typeof(UserAlreadyExistsException)))
            {
                context.Result = new ConflictObjectResult(message);
            }
            else if ((exceptionType == typeof(UsernameNotProvidedException)))
            {
                context.Result = new BadRequestObjectResult(message);
            }
            else if ((exceptionType == typeof(UserIsBlockedException)))
            {
                context.Result = new UnauthorizedObjectResult(message);
            }
            else if ((exceptionType == typeof(UnableToSendEmailException)))
            {
                context.Result = new BadRequestObjectResult(message);
            }
            else if ((exceptionType == typeof(OldPasswordIncorrectException)))
            {
                context.Result = new UnauthorizedObjectResult(message);
            }
            else
            {
                context.Result = new BadRequestObjectResult("Something went wrong");
            }
        }
    }
}

