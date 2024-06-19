using APIBrechoRFCC.Application.DTO.InputDTOs;
using FluentValidation;

namespace APIBrechoRFCC.Application.Validators
{
    public class ProductVariantValidator : AbstractValidator<ProductVariantInputDTO>
    {
        public ProductVariantValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("O título é obrigatório");
            RuleFor(p => p.ProductId).NotEmpty().WithMessage("O produto é obrigatório");
            RuleFor(p => p.OriginalPrice).NotEmpty().WithMessage("O valor original deve ser maior que 0.");
            RuleFor(p => p.SellingPrice).NotEmpty().WithMessage("O valor de venda deve ser maior que 0.");

        }
    }
}
