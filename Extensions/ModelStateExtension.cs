using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogAspNet.Extensions;

public static class ModelStateExtension
{
    public static List<string> GetErrors(this ModelStateDictionary modelState)
    {
        var result = new List<string>();
        foreach (var items in modelState.Values)
        {
            foreach (var error in items.Errors)
            {
                result.AddRange(items.Errors.Select(error => error.ErrorMessage));
            }
        }
        
        return result;
    }
}