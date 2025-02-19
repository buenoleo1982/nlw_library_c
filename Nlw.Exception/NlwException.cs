using System.Net;

namespace Nlw.Exception;

public abstract class NlwException : System.Exception
{
  public abstract List<string> GetErrorMessages();

  public abstract HttpStatusCode GetStatusCode();
}