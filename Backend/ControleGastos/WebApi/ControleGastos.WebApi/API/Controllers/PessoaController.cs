using ControleGastos.Application.Commands.Pessoa.Cadastrar;
using ControleGastos.WebApi.API.Requests.Pessoa;
using ControleGastos.WebApi.API.Responses.Pessoa;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.WebApi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EndpointGroupName("Pessoas")]
    public class PessoaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PessoaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]       
        public async Task<IResult> Cadastrar(CadastrarPessoaRequest request)
        {
            var command = request.Adapt<CadastrarPessoaCommand>();
            
            var result = await _mediator.Send(command);

            var response = result.Adapt<CadastrarPessoaResponse>();

            return Results.Created($"/pessoa/{response.Id}", response);
        }
    }
}
