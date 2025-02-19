using System.Net;

namespace Nlw.Exception;

public class InvalidLoginException : NlwException
{
  public override List<string> GetErrorMessages() => ["Email e/ou senha invalidos"];

  public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}