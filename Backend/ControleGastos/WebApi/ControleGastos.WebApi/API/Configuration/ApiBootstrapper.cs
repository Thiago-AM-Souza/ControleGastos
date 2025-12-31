using ControleGastos.WebApi.API.Middlewares;
using Microsoft.OpenApi;

namespace ControleGastos.WebApi.API.Configuration
{
    public static class ApiBootstrapper
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Controle de Gastos API", Version = "v1" });
                c.DocInclusionPredicate((_, api) => !string.IsNullOrEmpty(api.GroupName));

                c.TagActionsBy(api =>
                {
                    if (!string.IsNullOrEmpty(api.GroupName))
                    {
                        return [api.GroupName];
                    }

                    var endpointGroupName = api.ActionDescriptor.EndpointMetadata
                        .OfType<IEndpointGroupNameMetadata>()
                        .FirstOrDefault()?.EndpointGroupName;

                    return endpointGroupName != null ? [endpointGroupName] : new[] { "Outros" };
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddTransient<ExceptionHandlingMiddleware>();

            return services;
        }
    }
}
