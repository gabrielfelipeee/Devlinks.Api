using FluentValidation;

namespace Api.Service.Shared
{
    public class EntityFluentValidationService<TCreateDto, TUpdateDto>
    {
        private readonly IValidator<TCreateDto> _createValidator;
        private readonly IValidator<TUpdateDto> _updateValidator;
        public EntityFluentValidationService(IValidator<TCreateDto> createValidator, IValidator<TUpdateDto> updateValidator)
        {
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task ValidateCreateAsync(TCreateDto createDto)
        {
            var fluentValidationResult = await _createValidator.ValidateAsync(createDto);
            if (!fluentValidationResult.IsValid)
                throw new ValidationException(fluentValidationResult.Errors);
        }

        public async Task ValidateUpdateAsync(TUpdateDto updateDto)
        {
            var fluentValidationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!fluentValidationResult.IsValid)
                throw new ValidationException(fluentValidationResult.Errors);
        }
    }
}
