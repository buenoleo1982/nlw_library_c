using Microsoft.AspNetCore.Mvc;
using Nlw.Communication.Requests;
using Nlw.Communication.Responses;
using Nlw.Exception;
using NlwApi.UseCases.Users.Register;

namespace NlwApi.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : Controller
{
  [HttpPost]
  [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
  public IActionResult Register(RequestUserJson request)
  {
    var useCase = new RegisterUserUseCase();
    var response = useCase.Execute(request);

    return Created(string.Empty, response);
  }

  [HttpGet]
  public IActionResult Get()
  {
    return Created();
  }
}