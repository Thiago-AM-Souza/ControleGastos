using ControleGastos.Application.Exceptions.Abstractions;
using ControleGastos.Domain.Exceptions;
using FluentValidation;

namespace ControleGastos.WebApi.API.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await DispatchAsync(context, ex);
            }
        }

        // Tratar os tipos de exceção isoladamente
        private Task DispatchAsync(HttpContext context, Exception ex) =>
        ex switch
        {
            ValidationException validationEx =>
                HandleValidationAsync(context, validationEx),

            DomainException domainEx =>
                HandleDomainAsync(context, domainEx),

            ApplicationExceptionBase appEx =>
                HandleApplicationAsync(context, appEx),

            _ =>
                HandleUnexpectedAsync(context, ex)
        };

        private async Task HandleApplicationAsync(HttpContext context,
                                                  ApplicationExceptionBase ex)
        {
            context.Response.StatusCode = ex.StatusCode;

            await context.Response.WriteAsJsonAsync(new
            {
                type = "application_error",
                message = ex.Message
            });
        }


        private async Task HandleDomainAsync(HttpContext context,
                                             DomainException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(new
            {
                type = "domain_error",
                message = ex.Message
            });
        }

        private async Task HandleValidationAsync(HttpContext context,
                                                 ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(new
            {
                type = "validation_error",
                errors = ex.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                })
            });
        }

        private async Task HandleUnexpectedAsync(HttpContext context,
                                                 Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(new
            {
                error = "Erro interno no servidor"
            });
        }
    }
}
