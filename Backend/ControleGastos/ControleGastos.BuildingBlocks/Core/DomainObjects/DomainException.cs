namespace ControleGastos.BuildingBlocks.Core.DomainObjects
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message): base(message) { }
    }
}