using Api.Domain.Dtos.Link;
using FluentValidation;

namespace Api.Service.Validators.Link
{
    public class LinkUpdateValidator : LinkBaseValidator<LinkDtoUpdate>
    {
        public LinkUpdateValidator()
        {
            RuleFor(u => u.Id)
                .Must(BeValidGuid).WithMessage("O ID fornecido não é um Guid válido.")
                .NotEmpty().WithMessage("O ID do paciente é obigatórrio.");
        }

        // Função para validar o Guid
        private bool BeValidGuid(Guid id)
        {
            return id != Guid.Empty;
        }
    }
}
