using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; // Importa este espacio de nombres
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.BusinessLogic;

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
            services.AddScoped<IUserService, UserService>();
          

        }

        // Este método se utiliza para configurar cómo se manejarán las solicitudes HTTP entrantes.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configuraciones de middleware y enrutamiento aquí...
        }
    }
}
