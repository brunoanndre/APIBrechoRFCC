using APIBrechoRFCC.Application.DTO.InputDTOs;
using FluentValidation;

namespace APIBrechoRFCC.Application.Validators
{
    public class ProductOptionValidator : AbstractValidator<ProductOptionInputDTO>
    {
        public ProductOptionValidator() 
        {
            RuleFor(po => po.Name).NotEmpty().WithMessage("O nome é obrigatório.");
            RuleFor(po => po.Values).NotEmpty().WithMessage("A opção deve ter pelo menos um valor.");
            RuleFor(po => po.ProductId).NotEmpty().WithMessage("O produto é obrigatório.");
        }
    }
}
