using Entities;
using Microsoft.Extensions.Configuration;

namespace Repositories
{
    public interface IRatingRepository
    {
        Task<int> insertRate(Rating rating);
    }
}