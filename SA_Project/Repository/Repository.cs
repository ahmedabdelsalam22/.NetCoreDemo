using Microsoft.EntityFrameworkCore;
using SA_Project.Data;
using SA_Project.Repository.IRepository;
using System.Linq.Expressions;

namespace SA_Project.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }
        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<List<T>> GetAll(bool tracked = true)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked) 
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetById(bool tracked = true, Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null) 
            {
                query = query.Where(filter);
            }
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
