using ControleGastos.Application.Commands.Pessoa.Cadastrar;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ControleGastos.Application.Configurations.Modules
{
    public static class PessoaModule
    {
        public static IServiceCollection AddPessoaModule(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CadastrarPessoaCommand, CadastrarPessoaResult>, CadastrarPessoaCommandHandler>();

            return services;
        }
    }
}
