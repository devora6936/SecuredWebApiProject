using Entities;

namespace Services
{
    public interface IRatingService
    {
        Task<int> insertRate(Rating rating);
    }
}