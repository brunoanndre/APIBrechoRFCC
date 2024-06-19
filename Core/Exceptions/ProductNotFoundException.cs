namespace APIBrechoRFCC.Core.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int productId) : base($"Produto com o ID {productId} não foi encontrado.")
        {
            
        }
    }
}
