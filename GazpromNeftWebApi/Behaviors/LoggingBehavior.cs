using MediatR;
using System.Reflection;

namespace GazpromNeftWebApi.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.Trace($"Handling {typeof(TRequest).Name}");
            var properties = request.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(request);
                _logger.Trace($"{property.Name} : {propertyValue ?? "null"}");
            }
            return await next();
        }
    }
}
