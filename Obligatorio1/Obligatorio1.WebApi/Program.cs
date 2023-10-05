using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Obligatorio1.DataAccess.Contexts;
using Obligatorio1.DataAccess.Repositories;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace Obligatorio1.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure sessions
            builder.Services.AddDistributedMemoryCache(); // Opcional, para almacenar en caché la sesión en memoria
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Establece el tiempo de inactividad de la sesión
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // Si deseas que la sesión sea esencial para la aplicación
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Obligatorio 1", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath); // Esto incluirá el archivo XML de documentación en Swagger
            });

            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.IUserService, Obligatorio1.BusinessLogic.UserService>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IUserManagment, UserManagment>();
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.IPromoManagerService, Obligatorio1.BusinessLogic.PromoManagerService>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IPromoManagerManagment, PromoManagerManagment>();
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.ICartService, Obligatorio1.BusinessLogic.CartService>();
            builder.Services.AddDbContext<Context>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IGenericRepository<User>, Obligatorio1.DataAccess.Repositories.GenericRepository<User>>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Obligatorio 1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Habilita el middleware de sesiones
            app.UseSession();

            app.MapControllers();
            app.Run();
        }
    }
}
