using Obligatorio1.BusinessLogic;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;
using Obligatorio1.DataAccess;
using Obligatorio1.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors;

namespace Obligatorio1.ServiceFactory1;

    public class ServiceFactory
    {
        public ServiceFactory()
        {

        }

        public void RegistrateServices(IServiceCollection serviceCollection)
        {/*
                serviceCollection.AddDbContext<DbContext, Obligatorio1Context>();
                //serviceCollection.AddScoped<IGenericRepositort<Character>, Character>();
                //serviceCollection.AddScoped<ICharacterService, CharacterService>();*/

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
