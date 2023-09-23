using Microsoft.OpenApi.Models;

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
            });



            //Se configura que clase implementa a que interfaz
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.IUserService, Obligatorio1.BusinessLogic.UserService>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IUserManagment, Obligatorio1.DataAccess.UserManagment>();


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