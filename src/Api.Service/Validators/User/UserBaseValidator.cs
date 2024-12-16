using Api.Domain.Dtos.User;
using FluentValidation;

namespace Api.Service.Validators.User
{
    public class UserBaseValidator<T> : AbstractValidator<T> where T : UserBaseDto
    {
        public UserBaseValidator()
        {
            RuleFor(u => u.Name)
                 .NotEmpty().WithMessage("O NOME é obrigatório.")
                 .MinimumLength(3).WithMessage("O NOME deve ter pelo menos 3 caracteres.")
                 .MaximumLength(100).WithMessage("O NOME pode ter no máximo 100 caracteres.");
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O EMAIL é obrigatório.")
                .EmailAddress().WithMessage("O formato do EMAIL é inválido.")
                .MaximumLength(100).WithMessage("O EMAIL pode ter no máximo 100 caracteres");
        }
    }
}
