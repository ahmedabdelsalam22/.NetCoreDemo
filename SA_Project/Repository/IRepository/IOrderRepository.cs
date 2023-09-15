using SA_Project.Data;
using SA_Project.Models.Order;

namespace SA_Project.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order order);
    }
}
