using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MyLibraryContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(MyLibraryContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            Save();
        }
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                Save();
            }

        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
