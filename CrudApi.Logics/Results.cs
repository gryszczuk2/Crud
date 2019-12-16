using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using FluentValidation.Results;

namespace CrudApi.Logics
{
    public class Result
    {
        public bool IsSuccessful { get; set; }
        public IEnumerable<ErrorMessage> ErrorMessages= new List<ErrorMessage>();

        public static Result<T> Ok <T>(T value)
        {
            return new Result<T>
            {
                IsSuccessful = true,
                Value = value
            };
        }

        public static Result<T>Error<T>(string message)
        {
            return new Result<T>
            {
                IsSuccessful = false,
                ErrorMessages = new List<ErrorMessage>
                {
                    new ErrorMessage
                    {
                        PropertyName = string.Empty,
                        Message = message
                    }
                }
            };
        }

        public static Result<T> Error<T>(IEnumerable<ValidationFailure> validationFailures)
        {
            return new Result<T>
            {
                IsSuccessful =false,
                ErrorMessages = validationFailures.Select(v => new ErrorMessage()
                {
                    PropertyName = v.PropertyName,
                    Message = v.ErrorMessage

                })
            };
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }
    }
    public class ErrorMessage
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
