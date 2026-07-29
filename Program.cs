
using AISample.AIKernel.Extensions;
using AISample.Plugins;

using Microsoft.SemanticKernel;

//using Microsoft.SemanticKernel.ChatCompletion;
//using Microsoft.SemanticKernel.Connectors.OpenAI;

using AISample.WebApi.Extensions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OllamaSharp.Models;
using System.Net;
using Microsoft.SemanticKernel.ChatCompletion;



WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//var builder = Host.CreateApplicationBuilder(args);

builder
    .Services
    .AddAppCors()
    .AddAppOptions(builder.Configuration)
    .AddAppOllamaChatCompletion(builder.Configuration) // load before AddAppServices
    .AddAppLogging()
    .AddAppServices()
    .AddControllers();

WebApplication app = builder.Build();
app.MapControllers();

app.Run();




//bool isCloud = false;

//var model = "gpt-4.1";
//var endpoint = "https://aicafe.hcl.com/AICafeService/api/v1/subscription/openai/deployments/ADA/embeddings?api-version=2023-05-15";
////var endpoint = "https://aicafe.hcl.com/AICafeService/api/v1/subscription/openai/deployments/gpt-4.1/_chat/completions?api-version=2024-12-01-preview";
//var apiKey = "a30d585e-f703-4cbe-ad7a-b2693e59d269"; // hcl aicafe
//if (!isCloud)
//{
//    model = "qwen3.6";  // "qwen3.6" "gemma4"  "phi3" ;
//    endpoint = "http://localhost:11434/v1";
//    apiKey = "NOT_REQUIRED";
//}
//Console.WriteLine($"cloud: {isCloud} - Model: {model}");

//--------------------------------
//var builder = Kernel
//    .CreateBuilder()
//    .AddOpenAIChatCompletion(
//        //.AddAzureOpenAIChatCompletion(
//        modelId: model,
//        endpoint: new Uri(endpoint),
//        apiKey: apiKey

//    //model,
//    //endpoint,
//    //apiKey
//    );
//--------------------------------

//var reducer = new ChatHistoryTruncationReducer(3, 2); // ------- later

//// Enterprise-grade services. Logging service to the _kernel to help debug the AI agent.
//builder.Services.AddLogging(logging =>
//{
//    logging.AddConsole();
//    logging.SetMinimumLevel(LogLevel.Information);
//});
//--------------------------------

//Kernel _kernel = builder.Build();

//// Retrieve the _chat completion service
// IChatCompletionService chatCompletionService = _kernel.Services.GetRequiredService<IChatCompletionService>();
//--------------------------------

////give your AI agent the ability to run your code to retrieve information from external sources or to perform actions
//// Add a plugin (the LightsPlugin class is defined below)
//_kernel.Plugins.AddFromType<LightsPlugin>("Lights"); // --------- later

////To enable automatic function calling, we first need to create the appropriate execution settings so that Semantic Kernel knows to automatically invoke the functions in the _kernel when the AI agent requests them.
//// Enable planning
//OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new() // --------- later
//{
//    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
//};


//try
//{
//    var _chatHistory = new ChatHistory();

//    string userInput = string.Empty;

//    Console.WriteLine("Enter your message (or type 'exit' to quit):");
//    while (userInput != "exit")
//    {
//        Console.Write("User >>> ");
//        userInput = Console.ReadLine();
//        if (string.IsNullOrWhiteSpace(userInput) || userInput == "exit")
//        {
//            break;
//        }

//        // Reduce the _chat history to fit within the model's context window
//        //var reducedChatHistory = await reducer.ReduceAsync(_chatHistory);

//       // _chatHistory =  new ChatHistory(reducedChatHistory ?? Enumerable.Empty<ChatMessageContent>());

//        _chatHistory.AddUserMessage(userInput!);

//        //var result = await chatCompletionService.GetChatMessageContentAsync(
//        //    _chatHistory: _chatHistory,
//        //    executionSettings: openAIPromptExecutionSettings,
//        //    _kernel: _kernel);

//        //Console.WriteLine("Assistant >>> {0}", result);

//        Console.WriteLine("Assistant >> thinking...");

//        IAsyncEnumerable<StreamingChatMessageContent> resultStream = chatCompletionService.GetStreamingChatMessageContentsAsync(_chatHistory, openAIPromptExecutionSettings, _kernel);
//        await foreach (StreamingChatMessageContent s in resultStream)
//        {
//            Console.Write(s.Content);
//        }
//        Console.WriteLine(Environment.NewLine + "====");
//    }
//    ;
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"error: {ex.Message}");
//}