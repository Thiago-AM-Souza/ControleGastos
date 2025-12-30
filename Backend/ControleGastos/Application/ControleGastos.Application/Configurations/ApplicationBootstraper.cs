using ControleGastos.Application.Configurations.Modules;
using ControleGastos.BuildingBlocks.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ControleGastos.Application.Configurations
{
    public static class ApplicationBootstraper
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {            
            services.AddPessoaModule()
                    .AddCategoriaModule()
                    .AddTransacaoModule();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}
