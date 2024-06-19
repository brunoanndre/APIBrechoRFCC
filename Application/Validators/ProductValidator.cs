using APIBrechoRFCC.Application.DTO.InputDTOs;
using FluentValidation;

namespace APIBrechoRFCC.Application.Validators
{
    public class ProductValidator : AbstractValidator<ProductInputDTO>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("O título é obrigatório");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("O produto deve ter uma categoria");
        }
    }
}
