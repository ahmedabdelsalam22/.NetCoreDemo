using SA_Project.Web.Models;

namespace SA_Project.Web.Service
{
    public interface IOrderService
    {
        Task<T> GetAll<T>();
        Task<T> GetById<T>(int id);
        Task<T> Create<T>(OrderCreateDto orderCreateDto);
        Task<T> Delete<T>(int id);
    }
}
