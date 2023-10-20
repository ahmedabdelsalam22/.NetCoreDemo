using System.Linq.Expressions;

namespace SA_Project_API.Services.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(bool tracked = true);
        Task<T> GetById(bool tracked = true, Expression<Func<T, bool>>? filter = null);
        Task Create(T entity);
        void Delete(T entity);
    }
}
