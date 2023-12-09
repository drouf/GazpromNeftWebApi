using MediatR;

namespace GazpromNeftWebApi.Behaviors
{
    public class LoggingBehavour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Logger<LoggingBehavour<TRequest, TResponse>> _logger;

        public LoggingBehavour(Logger<LoggingBehavour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Before");
                return await next();
            }
            finally
            {
                _logger.Log(LogLevel.Information, "After");
            }
        }
    }
}
