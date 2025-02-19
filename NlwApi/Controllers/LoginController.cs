using Microsoft.AspNetCore.Mvc;
using Nlw.Communication.Requests;
using Nlw.Communication.Responses;
using Nlw.Exception;
using NlwApi.UseCases.Login.DoLogin;

namespace NlwApi.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status401Unauthorized)]
  public IActionResult DoLogin(RequestLoginJson request)
  {
    var useCase = new DoLoginUseCase();
    var response = useCase.Execute(request);
    return Ok(response);
  }
}