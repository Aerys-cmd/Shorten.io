
using Shorten.io.Infrastructure;
using Shorten.io.Application;
using Microsoft.Extensions.Configuration;
using Shorten.io.Middlewares.Expensely.Api.Middleware;

namespace Shorten.io;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddInfrastructureModule(builder.Configuration);

        builder.Services.AddApplicationModule(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseCors(configurePolicy =>
        {

            configurePolicy
                .WithOrigins()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

        app.UseCustomExceptionHandler();


        app.UseHttpsRedirection();

        //app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}