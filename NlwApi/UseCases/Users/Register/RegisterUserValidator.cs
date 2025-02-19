using FluentValidation;
using Nlw.Communication.Requests;

namespace NlwApi.UseCases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestUserJson>
{
  public RegisterUserValidator()
  {
    RuleFor(request => request.Name).NotEmpty().WithMessage("Nome é obrigatório");
    RuleFor(request => request.Email).EmailAddress().WithMessage("O email não é válido");
    RuleFor(request => request.Senha).NotEmpty().WithMessage("Senha é obrigatória");
    When(request => string.IsNullOrEmpty(request.Senha) == false, () =>
    {
      RuleFor(request => request.Senha.Length).GreaterThanOrEqualTo(6).WithMessage("Senha deve ser maior que 6 caracteres");
    });
  }
  
  
}