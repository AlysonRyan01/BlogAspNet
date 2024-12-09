using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogAspNet.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Query.TryGetValue(Configuration.ApiKeyname, out var extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "ApiKey nao encontrada"
            };
            return;
        }
    }
}