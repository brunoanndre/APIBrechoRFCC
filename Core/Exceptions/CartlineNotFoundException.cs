namespace APIBrechoRFCC.Core.Exceptions
{
    public class CartlineNotFoundException :Exception
    {
        public CartlineNotFoundException(int id) : base($"Cartline com o ID {id} não encontrado.") { }
    }
}
