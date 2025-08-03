using Microsoft.EntityFrameworkCore;
using SIGEBI.Infrastructure.Interfaces;
using SIGEBI.Infrastructure.Persistence.Context; 


namespace SIGEBI.Infrastructure.Persistence.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly SIGEBIContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(SIGEBIContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList(); // Retrieve all entities from the database
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id); // Find an entity by its primary key
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity); // Add a new entity to the database
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity); // Update an existing entity in the database
            _context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entity = _dbSet.Find(id); // Find the entity by its primary key
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}