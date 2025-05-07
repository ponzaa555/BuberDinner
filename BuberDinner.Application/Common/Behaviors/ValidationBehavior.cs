using BuberDinner.Application.Authentication.Command.Register;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;

namespace BuberDinner.Application.Common.Behaviors;

public class ValidationBehavior<TRequest,TResponse> : 
    IPipelineBehavior<TRequest , TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }   

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if(_validator is null)
        {
            return await next();
        }
        //before handler
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if(validationResult.IsValid)
        {
            return await next();
        }
        var errors =  validationResult.Errors
            .ConvertAll (x => Error.Validation(
                x.PropertyName,
                x.ErrorMessage)); 
        return (dynamic)errors;
        //after handler
    }
}