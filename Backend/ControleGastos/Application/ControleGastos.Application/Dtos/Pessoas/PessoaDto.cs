namespace ControleGastos.Application.Dtos.Pessoas
{
    public class PessoaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = default!;
        public int Idade { get; set; }

        public PessoaDto(Guid id,
                         string nome,
                         DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;            
            Idade = dataNascimento.ObterIdade();
        }
    }
}
