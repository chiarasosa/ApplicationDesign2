using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; // Importa este espacio de nombres
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.BusinessLogic;
using Obligatorio1.Domain;
using Obligatorio1.IDataAccess;
using Microsoft.OpenApi.Models;
using Obligatorio1.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Obligatorio1.DataAccess.Contexts;

namespace Obligatorio1.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método se utiliza para configurar los servicios que tu aplicación utilizará.
        
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuraciones de servicios aquí...
            services.AddScoped<Obligatorio1.IBusinessLogic.IUserService, Obligatorio1.BusinessLogic.UserService>();          
            services.AddScoped<Obligatorio1.IBusinessLogic.ICartService, Obligatorio1.BusinessLogic.CartService>();
            services.AddScoped<Obligatorio1.IBusinessLogic.IPurchaseService, Obligatorio1.BusinessLogic.PurchaseService>();
            services.AddScoped<Obligatorio1.IDataAccess.IPurchaseManagment, Obligatorio1.DataAccess.Repositories.PurchaseManagment>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Obligatorio 1", Version = "v1" });
            });


            // Registra el DbContext
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("obligatrioDB"));
            });

        }
        
        // Este método se utiliza para configurar cómo se manejarán las solicitudes HTTP entrantes.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
