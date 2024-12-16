using Api.Domain.Dtos.Link;
using FluentValidation;

namespace Api.Service.Validators.Link
{
    public class LinkCreateValidator : LinkBaseValidator<LinkDtoCreate>
    {
        public LinkCreateValidator()
        {}
    }
}
