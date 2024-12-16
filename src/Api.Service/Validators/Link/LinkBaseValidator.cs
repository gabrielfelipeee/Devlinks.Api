using Api.Domain.Dtos.Link;
using FluentValidation;

namespace Api.Service.Validators.Link
{
    public class LinkBaseValidator<T> : AbstractValidator<T> where T : LinkBaseDto
    {
        public LinkBaseValidator()
        {
            RuleFor(u => u.Platform)
                 .NotEmpty().WithMessage("O NOME da PLATAFORMA é obrigatório.")
                 .MaximumLength(45).WithMessage("O NOME da PLATAFORMA pode ter no máximo 45 caracteres.");
            RuleFor(u => u.Link)
                .NotEmpty().WithMessage("O LINK do seu perfil é obrigatório.")
                .MaximumLength(300).WithMessage("O LINK pode ter no máximo 300 caracteres");
        }
    }
}
