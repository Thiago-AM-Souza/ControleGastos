using ControleGastos.Domain.Pessoas;

namespace ControleGastos.Domain.Interfaces
{
    public interface IPessoaRepository
    {
        Task Cadastrar(Pessoa pessoa);
        Task Excluir(Pessoa pessoa);
        Task<Pessoa?> ObterPorId(Guid id);
        Task<IEnumerable<Pessoa>> Listar();
        Task<IEnumerable<Pessoa>> ListarPaginado(int index, int tamanho, CancellationToken cancellationToken);
        Task<long> ObterTotalCadastrados();
    }
}
