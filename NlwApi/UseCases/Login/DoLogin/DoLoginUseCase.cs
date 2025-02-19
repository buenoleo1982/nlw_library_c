using Nlw.Communication.Requests;
using Nlw.Communication.Responses;
using Nlw.Exception;
using NlwApi.Infraesructure.DataAccess;
using NlwApi.Infraesructure.Security.Criptography;
using NlwApi.Infraesructure.Security.Tokens.Access;

namespace NlwApi.UseCases.Login.DoLogin;

public class DoLoginUseCase
{
  public ResponseRegisteredUserJson Execute(RequestLoginJson request)
  {
    var dbContext = new NlwDbContext();
    var user = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));
    if (user is null)
    {
      throw new InvalidLoginException();
    }
    
    var criptography = new BcryptHash();
    var passwordIsValid = criptography.Verify(request.Senha, user);
    if (!passwordIsValid)
    {
      throw new InvalidLoginException();
    }
    var tokenGenerator = new JwtTokenGenerator();
    return new ResponseRegisteredUserJson
    {
      Name = user.Name,
      AcessToken = tokenGenerator.GenerateToken(user),
    };
  }
}