namespace APIBrechoRFCC.Core.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(int categoryId) :base($"Categoria com o ID {categoryId} não encontrada.") { }
    }
}
