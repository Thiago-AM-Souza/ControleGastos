using ControleGastos.Application.Exceptions.Abstractions;
using Microsoft.AspNetCore.Http;

namespace ControleGastos.Application.Exceptions
{
    public class NaoEncontradoException : ApplicationExceptionBase
    {
        public override int StatusCode => StatusCodes.Status404NotFound;
        public NaoEncontradoException(string message) : base(message)
        {
        }
    }
}
