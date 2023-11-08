// using System.ComponentModel.DataAnnotations;
using ProductHub.Application.Authentication.Commands.Register;
using ProductHub.Application.Authentication.Common;

namespace ProductHub.Application.Common.Behaviors;
using ErrorOr;
using MediatR;
using FluentValidation;

public class ValidationBehavior <TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;
    
    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator == null)
        {
            return await next();
        }
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors.Select(validationFailure =>
            Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage)).ToList();

        return (dynamic)errors;
    }

   
}