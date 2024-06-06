using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private MarketContext _marketContext;
        public ProductRepository(MarketContext marketContext)
        {
            _marketContext = marketContext;
        }

        public async Task<List<Product>> Get(int? position, int? skip, string? desc, double? minPrice, double? maxPrice, int?[] categoryIds)
        {
            var query = _marketContext.Products.Where(product =>
            (desc == null ? (true) : (product.Description.Contains(desc) || product.ProductName.Contains(desc)))
            && ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
            .OrderBy(product => product.Price);

            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.ToListAsync();
            return products; 
        }

        public async Task<double> GetPrice(int id)
        {
            Product p = await _marketContext.Products.FindAsync(id);
            return p.Price;
        }

    }
}
