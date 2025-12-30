using ControleGastos.Application.Transacoes.Commands.Cadastrar;
using ControleGastos.Application.Transacoes.Queries.Listar;
using ControleGastos.BuildingBlocks.Pagination;
using ControleGastos.WebApi.API.Requests.Transacao;
using ControleGastos.WebApi.API.Responses.Transacao;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.WebApi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EndpointGroupName("Transacoes")]
    public class TransacaoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IResult> Cadastrar(CadastrarTransacaoRequest request)
        {
            var command = request.Adapt<CadastrarTransacaoCommand>();

            var result = await _mediator.Send(command);

            var response = result.Adapt<CadastrarTransacaoResponse>();

            return Results.Created($"/transacao/{response.Id}", response);
        }

        [HttpGet]
        public async Task<IResult> Listar([FromQuery] PaginationRequest request)
        {
            var result = await _mediator.Send(new ListarTransacoesQuery(request));

            var response = result.Adapt<ListarTransacoesResponse>();

            return Results.Ok(response);
        }
    }
}
