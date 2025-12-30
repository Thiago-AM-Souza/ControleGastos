using ControleGastos.Application.Categorias.Commands.Cadastrar;
using ControleGastos.Application.Categorias.Queries.Listar;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ControleGastos.Application.Configurations.Modules
{
    public static class CategoriaModule
    {
        public static IServiceCollection AddCategoriaModule(this IServiceCollection services)
        {
            // Commands
            services.AddScoped<IRequestHandler<CadastrarCategoriaCommand, CadastrarCategoriaResult>, CadastrarCategoriaCommandHandler>();
            services.AddScoped<IRequestHandler<ListarCategoriasQuery, ListarCategoriasResult>, ListarCategoriasQueryHandler>();


            return services;
        }
    }
}
