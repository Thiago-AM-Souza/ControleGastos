using ControleGastos.Domain.Transacoes;

namespace ControleGastos.Domain.Interfaces
{
    public interface ITransacaoRepository
    {
        Task Cadastrar(Transacao transacao);
        Task<IEnumerable<Transacao>> Listar();        
        Task<IEnumerable<Transacao>> ListarPaginado(int index, int tamanho, CancellationToken cancellationToken);
        Task<long> ObterTotalCadastrados(); 
    }
}
