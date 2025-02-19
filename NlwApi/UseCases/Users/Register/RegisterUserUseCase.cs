using Nlw.Communication.Requests;
using Nlw.Communication.Responses;
using Nlw.Exception;
using NlwApi.Domain;
using NlwApi.Infraesructure;

namespace NlwApi.UseCases.Users.Register;

public class RegisterUserUseCase
{
  public ResponseRegisteredUserJson Execute(RequestUserJson request)
  {
    Validate(request);
    var entity = new User();
    entity.Email = request.Email;
    entity.Name = request.Name;
    entity.Senha = request.Senha;

    var dbContext = new NlwDbContext();
    dbContext.Users.Add(entity);
    dbContext.SaveChanges();
    return new ResponseRegisteredUserJson
    {
      Name = entity.Name
    };
  }

  private void Validate(RequestUserJson request)
  {
    var validator = new RegisterUserValidator();
    var result = validator.Validate(request);
    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(x => x.ErrorMessage);
      throw new ErrorOrValidationException(errorMessages);
    }
  }
}