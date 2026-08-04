using Api.Extensions;
using Infrastructure.SemanticKernel.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder
    .Services
    .AddAppCors()
    .AddAppOptions(builder.Configuration)
    .AddAppOllamaChatCompletion(builder.Configuration)
    .AddAppLogging()
    .AddAppServices()
    .AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.Run();