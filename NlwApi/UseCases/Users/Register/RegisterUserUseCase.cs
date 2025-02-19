using FluentValidation.Results;
using Nlw.Communication.Requests;
using Nlw.Communication.Responses;
using Nlw.Exception;
using NlwApi.Domain;
using NlwApi.Infraesructure.DataAccess;
using NlwApi.Infraesructure.Security.Criptography;
using NlwApi.Infraesructure.Security.Tokens.Access;

namespace NlwApi.UseCases.Users.Register;

public class RegisterUserUseCase
{
  public ResponseRegisteredUserJson Execute(RequestUserJson request)
  {
    var dbContext = new NlwDbContext();
    Validate(request, dbContext);
    var cryptography = new BcryptHash();
    var tokenGen = new JwtTokenGenerator();
    
    var entity = new User
    {
      Email = request.Email,
      Name = request.Name,
      Senha = cryptography.HashPassword(request.Senha)
    };
    
    dbContext.Users.Add(entity);
    dbContext.SaveChanges();
    return new ResponseRegisteredUserJson
    {
      Name = entity.Name,
      AcessToken = tokenGen.GenerateToken(entity)
    };
  }

  private void Validate(RequestUserJson request, NlwDbContext dbContext)
  {
    var validator = new RegisterUserValidator();
    var result = validator.Validate(request);
    var existUserWithEmail = dbContext.Users.Any(user => user.Email.Equals(request.Email));
    if (existUserWithEmail)
    {
      result.Errors.Add(new ValidationFailure("email", "Email is already in use."));
      
    }
    if (result.IsValid) return;
    var errorMessages = result.Errors.Select(x => x.ErrorMessage);
    throw new ErrorOrValidationException(errorMessages);
  }
}