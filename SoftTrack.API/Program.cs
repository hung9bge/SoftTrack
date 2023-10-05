using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SoftTrack.Application.DTO;
using SoftTrack.Application.Interface;
using SoftTrack.Application.Service;
using SoftTrack.Domain;
using SoftTrack.Infrastructure;

namespace SoftTrack.API
{
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

            // Mapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Repository
            builder.Services.AddScoped<ISoftwareRepository, SoftwareRepository>();

            // Service
            builder.Services.AddScoped<ISoftwareService, SoftwareService>();

            builder.Services.AddDbContext<soft_trackContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString")));

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
}