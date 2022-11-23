using CwkBooking.Api.Services;
using CwkBooking.Api.Services.Abstractions;

namespace CwkBooking.Api;

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
        builder.Services.AddSingleton<DataSource>();
        builder.Services.AddSingleton<MyFirstService>();

        builder.Services.AddSingleton<ISingletonOperation, SingletonOperation>();
        builder.Services.AddTransient<ITransientOperation, TransientOperation>();
        builder.Services.AddScoped<IScopedOperation, ScopedOperation>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

