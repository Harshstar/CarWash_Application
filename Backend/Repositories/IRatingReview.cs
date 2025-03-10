using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.Domain;

namespace carwash.Repositories
{
    public interface IRatingReview
    {
        Task<IEnumerable<RatingReview>> GetAllReviewsAsync();
        Task<RatingReview> GetReviewByIdAsync(Guid id);
        Task<IEnumerable<RatingReview>> GetReviewsByCustomerIdAsync(Guid customerId);
        Task<RatingReview> CreateReviewAsync(RatingReview obj);
        Task<RatingReview> UpdateReviewAsync(Guid id, RatingReview obj);
        Task<RatingReview> DeleteReviewAsync(Guid id);
    }
}