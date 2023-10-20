using SA_Project.Data;
using SA_Project.Models.Order;

namespace SA_Project_API.Services.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order order);
        Task Save();
    }
}
