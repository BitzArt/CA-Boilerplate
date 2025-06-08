using BitzArt;
using BitzArt.ApiExceptions;

namespace FluentValidation;

/// <summary>
/// Validation extension methods for API validation.
/// </summary>
public static class ApiValidateExtension
{
    /// <summary>
    /// Validates the specified value using the provided validator and throws a <see cref="BadRequestApiException"/> containing validation errors if validation fails.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validator">The validator to use.</param>
    /// <param name="value">The value to validate.</param>
    /// <param name="errorMessage">Error message to include in the exception if validation fails.</param>
    public static void ApiValidate<T>(this IValidator<T> validator, T value, string errorMessage = "Validation failed")
    {
        var validationResult = validator.Validate(value);
        if (!validationResult.IsValid)
        {
            var ex = ApiException.BadRequest(errorMessage);
            ex.Payload.AddData(new
            {
                errors = validationResult.Errors.Select(x => x.ErrorMessage)
            });
            throw ex;
        }
    }
}
