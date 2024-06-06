using Entities;
using Microsoft.Extensions.Logging;
using Repositories;
using System.ComponentModel;

namespace Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private ILogger<IOrderService> _logger;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, ILogger<IOrderService> logger)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Order> AddOrder(Order order)
        {
            if (await CheckOrderPrice(order))
                 return await _orderRepository.AddOrder(order);
            else
            {
                _logger.LogError($"user {order.UserId}  tried perchasing with a difffrent price {order.OrderSum}");
                return null;
            }
           
        }

        private async Task<double> GetPrice(int id)
        {
            return await _productRepository.GetPrice(id);
        }

        private async Task<bool> CheckOrderPrice(Order order)
        {
            double sum = 0;
            foreach (var item in order.OrderItems)
            {
                double price = await GetPrice(item.ProductId);

                sum += price * item.Quantity;

            }
            return order.OrderSum == sum;
        }

    }
}
