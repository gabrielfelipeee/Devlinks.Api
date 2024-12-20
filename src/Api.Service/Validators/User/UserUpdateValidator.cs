using Api.Domain.Dtos.User;
using FluentValidation;

namespace Api.Service.Validators.User
{
    public class UserUpdateValidator : UserBaseValidator<UserDtoUpdate>
    {
        public UserUpdateValidator()
        {
            RuleFor(u => u.Slug)
                .NotEmpty().WithMessage("O Slug é obrigatório.")
                .MinimumLength(3).WithMessage("O Slug deve ter pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("O Slug pode ter no máximo 100 caracteres.");
            RuleFor(u => u.Avatar)
                .NotEmpty().WithMessage("O Avatar é obrigatório.")
                .MaximumLength(300).WithMessage("O Avatar pode ter no máximo 300 caracteres");
        }
    }
}
