using Entities;

namespace Services
{
    public interface IProductService
    {
        Task<List<Product>> Get(int? position, int? skip, string? desc, double? minPrice, double? maxPrice, int?[] categoryIds);
    }
}