using FluentValidation;
using MediatR;

namespace TicketFlow.Service.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse> where TRequest: IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validator;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validator)
        => _validator = validator;
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validator.Any()) return await next(cancellationToken);
        var errors = _validator
            .Select(v => v.Validate(request))
            .SelectMany(r => r.Errors)
            .Where(e => e != null)
            .ToList();
        if (errors.Any()) throw new ValidationException(errors);
        return await next(cancellationToken);
    }
}