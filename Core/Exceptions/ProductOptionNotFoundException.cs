namespace APIBrechoRFCC.Core.Exceptions
{
    public class ProductOptionNotFoundException : Exception
    {
        public ProductOptionNotFoundException(int id) : base($"Opção de produto com o ID {id} não encontrada.")
        {
            
        }
    }
}
