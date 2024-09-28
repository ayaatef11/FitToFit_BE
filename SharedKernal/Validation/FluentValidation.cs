﻿using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using SharedKernal.ModuleInstaller;

namespace SharedKernal.Validation
{
    public static class FluentValidation// validate incoming models
    {
        public static void AddAppValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblies(ModularInstallerLoader.GetModulesAssemblies());
        }
        //Its purpose is to convert validation errors from the model state into a list of ValidationFailure objects
        public static List<ValidationFailure> ToFluentValidationFailures(this ModelStateDictionary modelState)
        {
            var errors = new List<ValidationFailure>();

            foreach (var keyValuePair in modelState)
            {
                foreach (var error in keyValuePair.Value.Errors)
                {
                    var errorMessage = string.IsNullOrEmpty(error.ErrorMessage) ? "Invalid data" : error.ErrorMessage;
                    var failure = new ValidationFailure(keyValuePair.Key, errorMessage)
                    {
                        AttemptedValue = keyValuePair.Value.RawValue,
                        ErrorMessage = errorMessage
                    };

                    errors.Add(failure);
                }
            }

            return errors;
        }
    }
}
