namespace ControleGastos.BuildingBlocks.Exceptions
{
    // se necessario as exceções podem definir seu proprio statuscode
    public interface IHttpMappedException
    {
        int StatusCode { get; }
    }
}
