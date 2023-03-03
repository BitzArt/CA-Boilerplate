using BitzArt;

namespace FluentValidation;

public static class ApiValidateExtension
{
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
