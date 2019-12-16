
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading;

namespace Moq
{
    public static class ValidatorExtension
    {
        public static void SetValidatorSuccess<T>(this Mock<IValidator<T>> validator)
        {
            validator.Setup(v => v.Validate(It.IsAny<T>()))
                .Returns(new ValidationResult());
        }


        public static void  SetValidatorFailure<T>(this Mock<IValidator<T>> validator,
            string errorMessage)
        {
            validator.Setup(v => v.Validate(It.IsAny<T>()))
                .Returns(new ValidationResult(new List<ValidationFailure>()
                {
                    new ValidationFailure(string.Empty,
                        errorMessage)
                }));
        }
    }
}
