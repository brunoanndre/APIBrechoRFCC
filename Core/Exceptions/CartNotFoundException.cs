namespace APIBrechoRFCC.Core.Exceptions
{
    public class CartNotFoundException : Exception
    {
        public CartNotFoundException(int id) : base($"O carrinho com ID {id} não foi encontrado.") { }
    }
}
