using CwkBooking.Api.Middleware;
using CwkBooking.Dal;
using CwkBooking.Dal.Repositories;
using CwkBooking.Domain.Abstractions.Repositories;
using CwkBooking.Domain.Abstractions.Services;
using CwkBooking.Services.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        var connectionStringBuilder = new SqlConnectionStringBuilder(
            builder.Configuration.GetConnectionString("DefaultConnection")
        );
        connectionStringBuilder.Password = builder.Configuration["DbPassword"];
        connectionStringBuilder.UserID = builder.Configuration["DbId"];

        var connection = connectionStringBuilder.ConnectionString;

        builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddScoped<IHotelsRepository, HotelRepository>();
        builder.Services.AddScoped<IReservationService, ReservationService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseDateTimeHeader();

        app.MapControllers();

        app.Run();
    }
}

