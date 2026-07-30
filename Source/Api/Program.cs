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

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
