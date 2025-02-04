using RDR2.Application;
using RDR2.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddTransient<ExceptionHandlingMiddleware>();
    builder.Services.AddControllers();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "RDR2 API",
            Version = "v1",
            Description = "RDR2 API"
        });
    });
}

var app = builder.Build();

{
    app.UseHttpsRedirection();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "RDR2 API v1");
        options.RoutePrefix = string.Empty;
    });

    app.MapControllers();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.Run();
}
