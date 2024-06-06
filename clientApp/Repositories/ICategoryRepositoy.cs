using Entities;

namespace Repositories
{
    public interface ICategoryRepositoy
    {
        Task<List<Category>> Get();
    }
}