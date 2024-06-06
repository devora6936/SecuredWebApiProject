using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> Get(int? position, int? skip, string? desc, double? minPrice, double? maxPrice, int?[] categoryIds);

        Task<double> GetPrice(int id);


    }
}