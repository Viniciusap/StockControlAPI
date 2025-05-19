using StockControlAPI.Controllers;
using StockControlAPI.Extensions;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.ConfigureSwagger();
        builder.Services.ConfigureDatabase(builder.Configuration);
        builder.Services.ConfigureDependencies();
        builder.Services.ConfigureAutoMapper();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockControlAPI v1"));
        }

        app.UseMiddleware<ApiExceptionMiddleware>();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}