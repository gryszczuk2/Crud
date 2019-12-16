using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudApi.Logics;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CrudApi.Logics
{
    public static class ResultExtension
    {
        public static void AddErrorToModelState(this Result result, ModelStateDictionary modelState)
        {
            if (result.IsSuccessful)
            {
                return;
            }

            foreach (var error in result.ErrorMessages)
            {
                modelState.AddModelError(error.PropertyName, error.Message);

            }
        }
    }
}
