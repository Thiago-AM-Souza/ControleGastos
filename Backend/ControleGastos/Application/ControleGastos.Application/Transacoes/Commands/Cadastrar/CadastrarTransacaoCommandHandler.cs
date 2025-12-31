
using ControleGastos.Domain.Categorias.Enums;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Domain.Transacoes;

namespace ControleGastos.Application.Transacoes.Commands.Cadastrar
{
    internal class CadastrarTransacaoCommandHandler
        : ICommandHandler<CadastrarTransacaoCommand, CadastrarTransacaoResult>
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public CadastrarTransacaoCommandHandler(ITransacaoRepository transacaoRepository,
                                                IPessoaRepository pessoaRepository,
                                                ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
            _pessoaRepository = pessoaRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task<CadastrarTransacaoResult> Handle(CadastrarTransacaoCommand command, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaRepository.ObterPorId(command.PessoaId);

            if (pessoa is null)
            {
                throw new NaoEncontradoException("Usuário não encontrado.");
            }

            var categoria = await _categoriaRepository.ObterPorId(command.CategoriaId);

            if (categoria is null)
            {
                throw new NaoEncontradoException("Categoria não encontrada.");
            }

            if (categoria.Finalidade != (Finalidade)command.Tipo
                && categoria.Finalidade != Finalidade.Ambas)
            {
                throw new FinalidadeInconsistenteException("Finalidade e Tipo de transação diferem.");
            }

            if (pessoa.DataNascimento.ObterIdade() < 18 
                && (Finalidade)command.Tipo != Finalidade.Despesa)
            {
                throw new FinalidadeInconsistenteException("Pessoas menores de 18 anos apenas geram despesas.");
            }

            var transacao = new Transacao(command.Descricao,
                                          command.Valor,
                                          (Finalidade)command.Tipo,
                                          pessoa,
                                          categoria);

            await _transacaoRepository.Cadastrar(transacao);

            return new CadastrarTransacaoResult(transacao.Id);
        }
    }
}
