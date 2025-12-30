using ControleGastos.Application.Transacoes.Commands.Cadastrar;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ControleGastos.Application.Configurations.Modules
{
    public static class TransacaoModule
    {
        public static IServiceCollection AddTransacaoModule(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CadastrarTransacaoCommand, CadastrarTransacaoResult>, CadastrarTransacaoCommandHandler>();

            return services;
        }
    }
}
