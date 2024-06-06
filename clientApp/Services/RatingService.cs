
using Entities;
using Repositories;



namespace Services
{
    public class RatingService : IRatingService
    {
        private IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        public async Task<int> insertRate(Rating rating)
        {
            return await _ratingRepository.insertRate(rating);
        }



    }
}

