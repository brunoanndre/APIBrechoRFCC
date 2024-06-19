namespace APIBrechoRFCC.Core.Exceptions
{
    public class ProductVariantNotFoundException : Exception
    {
        public ProductVariantNotFoundException(int id) : base($"Variante do produto com ID {id} não encontrada.")
        {

        }
    }
}
