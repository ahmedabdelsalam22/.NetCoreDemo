using SA_Project.Data;
using SA_Project.Models.Order;
using SA_Project.Repository.IRepository;

namespace SA_Project.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Order order)
        {
            _db.Update(order);
        }
    }
}
