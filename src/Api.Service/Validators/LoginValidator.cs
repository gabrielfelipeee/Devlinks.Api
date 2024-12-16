using Api.Domain.Dtos;
using FluentValidation;

namespace Api.Service.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(u => u.Email)
               .NotEmpty().WithMessage("O EMAIL é obrigatório.")
               .EmailAddress().WithMessage("O formato do EMAIL é inválido.")
               .MaximumLength(100).WithMessage("O EMAIL pode ter no máximo 100 caracteres");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
                .MaximumLength(60).WithMessage("A senha pode ter no máximo 60 caracteres.")
                .Matches(@"[!@#$%^&*(),.?""':;{}|<>]").WithMessage("A senha deve conter pelo menos um caractere especial");
        }
    }
}
