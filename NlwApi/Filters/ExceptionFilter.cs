using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nlw.Communication.Responses;
using Nlw.Exception;

namespace NlwApi.Filters;

public class ExceptionFilter : IExceptionFilter
{
  public void OnException(ExceptionContext context)
  {
    if (context.Exception is NlwException nlwException)
    {
      context.HttpContext.Response.StatusCode = (int)nlwException.GetStatusCode();
      context.Result = new ObjectResult(new ResponseErrorMessageJson
      {
        Errors = nlwException.GetErrorMessages()
      });
    }
    else
    {
      context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
      context.Result = new ObjectResult(new ResponseErrorMessageJson
      {
        Errors = ["Internal Server Error"]
      });
    }
  }
}