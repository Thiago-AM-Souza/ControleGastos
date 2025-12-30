using ControleGastos.Application.Exceptions.Abstractions;
using Microsoft.AspNetCore.Http;

namespace ControleGastos.Application.Exceptions
{
    public class FinalidadeInconsistenteException : ApplicationExceptionBase
    {
        public override int StatusCode => StatusCodes.Status409Conflict;
        public FinalidadeInconsistenteException(string message) : base(message)
        {
        }
    }
}
