using ControleGastos.Domain.Categorias;

namespace ControleGastos.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task Cadastrar(Categoria categoria);
        Task<Categoria?> ObterPorId(Guid id);
        Task<Categoria?> ObterPorDescricao(string descricao);
        Task Atualizar(Categoria categoria);
        Task<IEnumerable<Categoria>> Listar();
        Task<IEnumerable<Categoria>> ListarPaginado(int index, int tamanho, CancellationToken cancellationToken);
        Task<long> ObterTotalCadastrados();
    }
}
