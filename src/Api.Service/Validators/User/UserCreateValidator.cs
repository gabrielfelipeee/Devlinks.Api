using Api.Domain.Dtos.User;
using FluentValidation;

namespace Api.Service.Validators.User
{
    public class UserCreateValidator : UserBaseValidator<UserDtoCreate>
    {
        public UserCreateValidator()
        {
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
                .MaximumLength(60).WithMessage("A senha pode ter no máximo 60 caracteres.")
                .Matches(@"[!@#$%^&*(),.?""':;{}|<>]").WithMessage("A senha deve conter pelo menos um caractere especial");
        }
    }
}
