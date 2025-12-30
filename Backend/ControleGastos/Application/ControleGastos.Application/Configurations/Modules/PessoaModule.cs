using ControleGastos.Application.Pessoas.Commands.Cadastrar;
using ControleGastos.Application.Pessoas.Commands.Deletar;
using ControleGastos.Application.Pessoas.Queries.Listar;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ControleGastos.Application.Configurations.Modules
{
    public static class PessoaModule
    {
        public static IServiceCollection AddPessoaModule(this IServiceCollection services)
        {
            // Commands
            services.AddScoped<IRequestHandler<CadastrarPessoaCommand, CadastrarPessoaResult>, CadastrarPessoaCommandHandler>();
            services.AddScoped<IRequestHandler<DeletarPessoaCommand, DeletarPessoaResult>, DeletarPessoaCommandHandler>();

            // Queries
            services.AddScoped<IRequestHandler<ListarPessoasQuery, ListarPessoasResult>, ListarPessoasQueryHandler>();            

            return services;
        }
    }
}
