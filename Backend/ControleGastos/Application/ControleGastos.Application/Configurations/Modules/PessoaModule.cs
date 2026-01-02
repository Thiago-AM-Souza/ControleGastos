using ControleGastos.Application.Pessoas.Commands.Cadastrar;
using ControleGastos.Application.Pessoas.Commands.Deletar;
using ControleGastos.Application.Pessoas.Queries.ConsultaTotaisPorPessoa;
using ControleGastos.Application.Pessoas.Queries.Listar;
using ControleGastos.Application.Pessoas.Queries.ListarTodos;
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
            services.AddScoped<IRequestHandler<ConsultaTotaisPorPessoaQuery, IReadOnlyList<ConsultaTotaisPorPessoaResult>>, ConsultaTotaisPorPessoaQueryHandler>();
            services.AddScoped<IRequestHandler<ListarTodasPessoasQuery, ListarTodasPessoasResult>, ListarTodasPessoasQueryHandler>();

            return services;
        }
    }
}
