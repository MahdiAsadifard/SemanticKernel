namespace AISample.WebApi.Extensions
{
    public static class CorsExtension
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAppCors()
            { 
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                    {
                        policy
                        //.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                    });
                });
                return services;
            }
        }
    }
}
