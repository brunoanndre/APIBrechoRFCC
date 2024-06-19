namespace APIBrechoRFCC.Core.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(Guid id) :base($"Pedido com o ID {id} não foi encontrado.") { }
    }
}
