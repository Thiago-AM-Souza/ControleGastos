using ControleGastos.Infrastructure;

namespace ControleGastos.WebApi.Configuration
{
    public static class AppBootstrapper
    {
        public static IServiceCollection AddAppBootstrapper(this IServiceCollection services,
                                                            IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);

            return services;
        }
    }
}
