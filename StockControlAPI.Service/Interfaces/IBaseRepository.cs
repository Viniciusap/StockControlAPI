namespace StockControlAPI.Service.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Add(T entity);
        List<T> GetAll();
        T GetById(int id);
        T Update(T entity);
        bool Delete(T entity);
    }
}