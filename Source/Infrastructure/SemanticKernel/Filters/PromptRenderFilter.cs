using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SemanticKernel.Filters
{
    public sealed class PromptRenderFilter : IPromptRenderFilter
    {
        private readonly ILogger<PromptRenderFilter> _logger;

        public PromptRenderFilter(ILogger<PromptRenderFilter> logger)
        {
            this._logger = logger;
        }

        public async Task OnPromptRenderAsync(PromptRenderContext context, Func<PromptRenderContext, Task> next)
        {
            _logger.LogInformation("Invoking prompt: {FunctionName} from plugin: {PluginName}, RenderedPrompt: {RenderedPrompt}", context.Function.Name, context.Function.PluginName, context.RenderedPrompt);

            await next(context);

            _logger.LogInformation("Invoked prompt: {FunctionName} from plugin: {PluginName}, RenderedPrompt: {RenderedPrompt}", context.Function.Name, context.Function.PluginName, context.RenderedPrompt);
        }
    }
}
