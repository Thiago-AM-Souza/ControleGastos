using ControleGastos.Application.Pessoas.Queries;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Database;
using ControleGastos.Infrastructure.Repositories.Categorias;
using ControleGastos.Infrastructure.Repositories.Pessoas;
using ControleGastos.Infrastructure.Repositories.Transacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace ControleGastos.Infrastructure
{
    public static class InfrastructureBootstrapper
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            // Banco de dados
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // EF Core
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            // Dapper
            services.AddScoped<IDbConnection>(_ =>
                new NpgsqlConnection(connectionString));

            // Repositorios
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<IPessoaQueryRepository, PessoaQueryRepository>();

            return services;
        }
    }
}
