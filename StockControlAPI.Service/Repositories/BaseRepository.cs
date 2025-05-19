using Microsoft.EntityFrameworkCore;
using StockControlAPI.Infrastruture.Data;
using StockControlAPI.Service.Interfaces;

namespace StockControlAPI.Service.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly StockControlAPIContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(StockControlAPIContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public List<T> GetAll()
        {
            return [.. _dbSet];
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}