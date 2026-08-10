using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SemanticKernel.Filters
{

    /// <summary>
    /// A filter that logs the invocation of a function in the Semantic Kernel.
    /// Filtering actions during function invocation.
    /// </summary>
    public sealed class LoggingFunctionFilter : IFunctionInvocationFilter
    {
        private readonly ILogger<LoggingFunctionFilter> _logger;

        public LoggingFunctionFilter(ILogger<LoggingFunctionFilter> logger)
        {
            this._logger = logger;
        }
        public async Task OnFunctionInvocationAsync(FunctionInvocationContext context, Func<FunctionInvocationContext, Task> next)
        {
            _logger.LogInformation("Invoking function: {FunctionName} from plugin: {PluginName}", context.Function.Name, context.Function.PluginName);
        
            await next(context);
            
            _logger.LogInformation("Function invocation completed: {FunctionName}", context.Function.Name);
        }
    }
}
