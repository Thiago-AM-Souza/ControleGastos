namespace ControleGastos.WebApi.API.Requests.Categoria
{
    public class CadastrarCategoriaRequest
    {
        public string Descricao { get; set; } = default!;
        public int Finalidade { get; set; }
    }
}
