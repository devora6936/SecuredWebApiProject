using Entities;
using Repositories;


namespace Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Product>> Get(int? position, int? skip, string? desc, double? minPrice, double? maxPrice, int?[] categoryIds)
        {
            return await _productRepository.Get(position, skip, desc, minPrice, maxPrice, categoryIds);
        }

    }
}
