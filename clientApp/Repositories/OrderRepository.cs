using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private MarketContext _marketContext;
        public OrderRepository(MarketContext marketContext)
        {
            _marketContext = marketContext;
        }

        public async Task<Order> AddOrder(Order order)
        {
            await _marketContext.Orders.AddAsync(order);
            await _marketContext.SaveChangesAsync();
            return order;
        }



    }
}
