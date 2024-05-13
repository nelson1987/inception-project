using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace Inception.Api.Extensions;

public static class FluentValidationExtensions
{
    public static bool IsInvalid(this ValidationResult result)
    {
        return !result.IsValid;
    }

    public static ModelStateDictionary ToModelState(this ValidationResult result)
    {
        var modelState = new ModelStateDictionary();

        foreach (var error in result.Errors)
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        return modelState;
    }
}
public static class JsonSerializerExtensions
{
    public static string ToJson(this object result)
    {
        return JsonSerializer.Serialize(result);
    }
}