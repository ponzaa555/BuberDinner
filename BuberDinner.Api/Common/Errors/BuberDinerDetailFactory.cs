using System.Diagnostics;
using BuberDinner.Api.Common.Http;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace BuberDinner.Api.Common.Errors;

// this copy paste from Microsoft.AspNetCore.Mvc.Infrastructure.DefaultProblemDetailsFactory
public class  BuberDinerDetailFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    public BuberDinerDetailFactory(IOptions<ApiBehaviorOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 500;
        var ProblemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };

        ApplyProblemDetailsDefaults(httpContext, ProblemDetails, statusCode.Value);

        return ProblemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
    HttpContext httpContext,
    ModelStateDictionary modelStateDictionary,
    int? statusCode = null,
    string? title = null,
    string? type = null,
    string? detail = null,
    string? instance = null)
    {
        ArgumentNullException.ThrowIfNull(modelStateDictionary);

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (title != null)
        {
            // For validation problem details, don't overwrite the default title with null.
            problemDetails.Title = title;
        }

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext context, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;
        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientError))
        {
            problemDetails.Title ??= clientError.Title;
            problemDetails.Type ??= clientError.Link;
        }
        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }
        var errors = context?.Items[HttpContextItemKeys.Errors] as List<Error>;
        if(errors is not null)
        {
            problemDetails.Extensions.Add("errorsCodes" , errors.Select(e => $"{e.Code} : {e.Description}"));
        } 
    }
}