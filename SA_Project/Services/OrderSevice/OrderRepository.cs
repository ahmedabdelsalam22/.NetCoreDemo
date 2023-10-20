using SA_Project.Data;
using SA_Project.Models.Order;
using SA_Project_API.Services.Repository.IRepository;

namespace SA_Project_API.Services.Repository
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
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
