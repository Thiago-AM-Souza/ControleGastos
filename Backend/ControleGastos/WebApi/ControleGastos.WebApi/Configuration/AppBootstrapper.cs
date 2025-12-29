using ControleGastos.Application.Configurations;
using ControleGastos.Infrastructure;
using ControleGastos.WebApi.API.Configuration;

namespace ControleGastos.WebApi.Configuration
{
    // Bootstrapper pattern utilizado para que cada modulo
    // do sistema fique responsavel por suas proprias configuracoes
    public static class AppBootstrapper
    {
        public static IServiceCollection AddAppBootstrapper(this IServiceCollection services,
                                                            IConfiguration configuration)
        {
            services.AddInfrastructure(configuration)
                    .AddApplication()
                    .AddApi();

            return services;
        }
    }
}
