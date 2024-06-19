using FluentValidation;
using APIBrechoRFCC.Application.DTO.InputDTOs;

namespace APIBrechoRFCC.Application.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryInputDTO>
    {

        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O nome da categoria é obrigatório");

        }
    }
}
