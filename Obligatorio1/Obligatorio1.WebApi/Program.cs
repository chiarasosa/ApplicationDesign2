using Microsoft.OpenApi.Models;
using Obligatorio1.DataAccess.Repositories;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Obligatorio 1", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath); // Esto incluir� el archivo XML de documentaci�n en Swagger
            });
            //Se configura que clase implementa a que interfaz

            
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.IUserService, Obligatorio1.BusinessLogic.UserService>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IUserManagment, Obligatorio1.DataAccess.Repositories.UserManagment>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IPromoManagerManagment, Obligatorio1.DataAccess.Repositories.PromoManagerManagment>();
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.ICartService, Obligatorio1.BusinessLogic.CartService>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.ICartManagment, Obligatorio1.DataAccess.CartManagment>();
            builder.Services.AddScoped<Obligatorio1.Domain.User>();
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.IPurchaseService, Obligatorio1.BusinessLogic.PurchaseService>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IPurchaseManagment, Obligatorio1.DataAccess.Repositories.PurchaseManagment>();

            //var serviceFactory = new Obligatorio1.ServiceFactory.ServiceFactory();
            //serviceFactory.RegistrateServices(builder.Services);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Obligatorio 1");
                });

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}