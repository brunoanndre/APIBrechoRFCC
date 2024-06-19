namespace APIBrechoRFCC.Infrastructure.Interface
{
    public interface ICRUDRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T model);
        Task<T>? Update(T model);
        Task<bool> Delete(int id);
    }
}
