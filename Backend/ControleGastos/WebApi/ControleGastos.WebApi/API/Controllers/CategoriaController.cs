using ControleGastos.Application.Categorias.Commands.Cadastrar;
using ControleGastos.Application.Categorias.Queries.ConsultaTotaisPorCategoria;
using ControleGastos.Application.Categorias.Queries.Listar;
using ControleGastos.Application.Categorias.Queries.ListarTodos;
using ControleGastos.BuildingBlocks.Pagination;
using ControleGastos.WebApi.API.Requests.Categoria;
using ControleGastos.WebApi.API.Responses.Categoria;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.WebApi.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EndpointGroupName("Categorias")]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IResult> Cadastrar(CadastrarCategoriaRequest request)
        {
            var command = request.Adapt<CadastrarCategoriaCommand>();

            var result = await _mediator.Send(command);

            var response = result.Adapt<CadastrarCategoriaResponse>();

            return Results.Created($"/categoria/{response.Id}", response); 
        }

        [HttpGet]
        public async Task<IResult> Listar([FromQuery] PaginationRequest request)
        {
            var result = await _mediator.Send(new ListarCategoriasQuery(request));

            var response = result.Adapt<ListarCategoriasResponse>();

            return Results.Ok(response);
        }

        [HttpGet("listar-todos")]
        public async Task<IResult> ListarTodos()
        {
            var result = await _mediator.Send(new ListarTodasCategoriasQuery());

            return Results.Ok(result.Categorias);
        }

        [HttpGet("consultar-totais")]
        public async Task<IResult> ConsultarTotalPorCategoria()
        {
            var result = await _mediator.Send(new ConsultaTotaisPorCategoriaQuery());

            return Results.Ok(result);
        }
    }
}
