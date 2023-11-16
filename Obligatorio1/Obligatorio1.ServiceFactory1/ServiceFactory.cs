using Microsoft.Extensions.DependencyInjection;

namespace Obligatorio1.ServiceFactory1;

public class ServiceFactory
{
    public ServiceFactory()
    {

    }

    public void RegistrateServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }

            );
        }

        );
    }
}
