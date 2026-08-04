using Infrastructure.SemanticKernel.Plugins;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Infrastructure.SemanticKernel.Helpers
{
    public class AIHelpers
    {
        public static Kernel AddPlugins(Kernel kernel)
        {
            var pluginTypes =
                typeof(IKernelPlugin)
                .Assembly
                .GetTypes()
                .Where(type =>
                    type is { IsClass: true, IsAbstract: false } &&
                    typeof(IKernelPlugin).IsAssignableFrom(type)
                );

            // Get the AddFromType extension method using reflection
            var addFromTypeMethod = typeof(KernelExtensions)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(m =>
                    m.Name == "AddFromType" &&
                    m.IsGenericMethodDefinition &&
                    m.GetParameters().Length == 3);

            if (addFromTypeMethod == null)
            {
                throw new InvalidOperationException("Could not find AddFromType method.");
            }

            foreach (var pluginType in pluginTypes)
            {
                // Create a generic method with the specific plugin type
                var genericMethod = addFromTypeMethod.MakeGenericMethod(pluginType);
                
                // Invoke the extension method (first param is the collection, then pluginName, then serviceProvider)
                genericMethod.Invoke(null, new object?[] { kernel.Plugins, null, kernel.Services });
            }
            
            return kernel;
        }
    }
}
