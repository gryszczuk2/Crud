using CrudApi.Models;
using FluentValidation;

namespace CrudApi.Logics.Validators
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(10);
            RuleFor(p => p.Price)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
