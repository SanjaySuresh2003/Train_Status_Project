using Microsoft.EntityFrameworkCore;
using Train_Status_API.Data;

namespace Train_Status_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TrainBookingDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TrainBookingDB")));

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            //app.MapGet("/test-db", async (TrainBookingDbContext db) =>
            //{
            //    var stationCount = await db.Stations.CountAsync();
            //    return Results.Ok(new { Message = "Connected!", StationCount = stationCount });
            //});

            app.Run();
        }
    }
}
