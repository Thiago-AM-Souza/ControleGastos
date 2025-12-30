using ControleGastos.Domain.Categorias;

namespace ControleGastos.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task Cadastrar(Categoria categoria);
        Task<IEnumerable<Categoria>> Listar();
        Task<IEnumerable<Categoria>> ListarPaginado(int index, int tamanho, CancellationToken cancellationToken);
        Task<long> ObterTotalCadastrados();
    }
}
