using ControleGastos.Application.Pessoas.Commands.Cadastrar;
using ControleGastos.Application.Pessoas.Commands.Deletar;
using ControleGastos.Application.Pessoas.Queries.ConsultaTotaisPorPessoa;
using ControleGastos.Application.Pessoas.Queries.Listar;
using ControleGastos.Application.Pessoas.Queries.ListarTodos;
using ControleGastos.BuildingBlocks.Pagination;
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

        [HttpDelete]
        public async Task<IResult> Deletar(Guid id)
        {
            var command = new DeletarPessoaCommand(id);
            
            var result = await _mediator.Send(command);

            return Results.Ok(result.Sucesso);
        }

        [HttpGet]
        public async Task<IResult> Listar([FromQuery] PaginationRequest request)
        {
            var result = await _mediator.Send(new ListarPessoasQuery(request));

            var response = new ListarPessoasResponse(result.Pessoas);

            return Results.Ok(response);
        }

        [HttpGet("listar-todos")]
        public async Task<IResult> ListarTodos()
        {
            var result = await _mediator.Send(new ListarTodasPessoasQuery());

            return Results.Ok(result.Pessoas);
        }

        [HttpGet("consultar-totais")]
        public async Task<IResult> ConsultarTotalPorPessoa()
        {
            var result = await _mediator.Send(new ConsultaTotaisPorPessoaQuery());

            return Results.Ok(result);
        }
    }
}
