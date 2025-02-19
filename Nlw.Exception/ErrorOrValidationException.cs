using System.Net;

namespace Nlw.Exception;

public class ErrorOrValidationException : NlwException
{
  private readonly List<string> _errors;
  public ErrorOrValidationException(IEnumerable<string> errorMessage)
  {
    _errors = errorMessage.ToList();
  }
  public override List<string> GetErrorMessages() => _errors;

  public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}