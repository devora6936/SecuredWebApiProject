
using Entities;
using Repositories;



namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepositoy _CategoryRepository;
        public CategoryService(ICategoryRepositoy CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public async Task<List<Category>>Get()
        {
            return await _CategoryRepository.Get();
        }



    }
}

