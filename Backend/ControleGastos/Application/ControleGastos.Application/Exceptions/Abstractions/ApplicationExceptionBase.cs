using Microsoft.AspNetCore.Http;

namespace ControleGastos.Application.Exceptions.Abstractions
{
    public abstract class ApplicationExceptionBase : Exception, IHttpMappedException
    {
        public virtual int StatusCode => StatusCodes.Status422UnprocessableEntity;
        protected ApplicationExceptionBase(string message) : base(message) { }

    }
}
