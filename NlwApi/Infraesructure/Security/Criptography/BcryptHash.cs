using NlwApi.Domain;

namespace NlwApi.Infraesructure.Security.Criptography;

public class BcryptHash
{
  public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
  
  public bool Verify(string password, User user) => BCrypt.Net.BCrypt.Verify(password, user.Senha);
}