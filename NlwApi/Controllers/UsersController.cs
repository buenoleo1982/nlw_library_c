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
  public IActionResult Create(RequestUserJson request)
  {
    try
    {
      var useCase = new RegisterUserUseCase();
      var response = useCase.Execute(request);

      return Created(string.Empty, response);
    }
    catch (NlwException e)
    {
      return BadRequest(new ResponseErrorMessageJson
      {
        Errors = e.GetErrorMessages()
      });
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError,new ResponseErrorMessageJson
      {
        Errors = ["Internal Server Error" + e.Message]
      });
    }
  }

  [HttpGet]
  public IActionResult Get()
  {
    return Created();
  }
}