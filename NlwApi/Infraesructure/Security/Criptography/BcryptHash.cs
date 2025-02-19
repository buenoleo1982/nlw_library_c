namespace NlwApi.Infraesructure.Security.Criptography;

public class BcryptHash
{
  public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
}