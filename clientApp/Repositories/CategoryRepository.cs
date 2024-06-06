using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Repositories
{
    public class CategoryRepositoy : ICategoryRepositoy
    {
        private MarketContext _marketContext;
        public CategoryRepositoy(MarketContext marketContext)
        {
            _marketContext = marketContext;
        }

        public async Task<List<Category>> Get()
        {
            return await _marketContext.Categories.ToListAsync();
        }

    }
}
