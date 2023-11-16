using Microsoft.OpenApi.Models;
using Obligatorio1.DataAccess.Contexts;
using Obligatorio1.Domain;
using Obligatorio1.ServiceFactory1;
using Obligatorio1.WebApi.Filters;
using System.Reflection;

namespace Obligatorio1.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Obligatorio 1", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.IUserService, Obligatorio1.BusinessLogic.UserService>();
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.IProductService, Obligatorio1.BusinessLogic.ProductService>();
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.ICartService, Obligatorio1.BusinessLogic.CartService>();
            builder.Services.AddDbContext<Context>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IGenericRepository<User>, Obligatorio1.DataAccess.Repositories.GenericRepository<User>>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IGenericRepository<Product>, Obligatorio1.DataAccess.Repositories.GenericRepository<Product>>();
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.ISessionService, Obligatorio1.BusinessLogic.SessionService>();
            builder.Services.AddScoped<Obligatorio1.WebApi.Filters.ExceptionFilter>();
            builder.Services.AddScoped<Obligatorio1.WebApi.Filters.AuthenticationFilter>();
            builder.Services.AddScoped<Obligatorio1.WebApi.Filters.AuthorizationRolFilter>();
            builder.Services.AddScoped<AuthorizationRolFilter>();
            builder.Services.AddScoped<Obligatorio1.Domain.User>();
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.IPurchaseService, Obligatorio1.BusinessLogic.PurchaseService>();
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.IPromotionsService, Obligatorio1.BusinessLogic.PromotionsService>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IGenericRepository<Purchase>, Obligatorio1.DataAccess.Repositories.GenericRepository<Purchase>>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IGenericRepository<Cart>, Obligatorio1.DataAccess.Repositories.GenericRepository<Cart>>();
            builder.Services.AddScoped<Obligatorio1.IBusinessLogic.ISessionService, Obligatorio1.BusinessLogic.SessionService>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IGenericRepository<Session>, Obligatorio1.DataAccess.Repositories.GenericRepository<Session>>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IGenericRepository<CartProduct>, Obligatorio1.DataAccess.Repositories.GenericRepository<CartProduct>>();
            builder.Services.AddScoped<Obligatorio1.IDataAccess.IGenericRepository<PurchaseProduct>, Obligatorio1.DataAccess.Repositories.GenericRepository<PurchaseProduct>>();

            var servicesFactory = new ServiceFactory();
            servicesFactory.RegistrateServices(builder.Services);

            var app = builder.Build();
            app.UseCors();

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

            app.UseSession();

            app.MapControllers();
            app.Run();
        }
    }
}

//