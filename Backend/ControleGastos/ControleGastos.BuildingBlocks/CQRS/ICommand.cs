using MediatR;

namespace ControleGastos.BuildingBlocks.CQRS
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse> 
    { 
    }
}
