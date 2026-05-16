using Domain.Products.Dto;
using FluentValidation;

namespace Domain.Products.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(product => product.Description).MaximumLength(150);
            RuleFor(product => product.Price).GreaterThanOrEqualTo(0);
        }
    }
}
