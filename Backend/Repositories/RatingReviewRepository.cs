using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Data;
using carwash.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace carwash.Repositories
{
    public class RatingReviewRepository : IRatingReview
    {
        public CarwashDbContext _db;
        public RatingReviewRepository(CarwashDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<RatingReview>> GetAllReviewsAsync()
        {
            var res = await _db.RatingReviews.Include(x=>x.Washer).Include(x=>x.Customer).ToListAsync();
            return res;
        }

        public async Task<RatingReview> GetReviewByIdAsync(Guid id)
        {
            var res = await _db.RatingReviews.Include(x=>x.Washer).Include(x=>x.Customer).FirstOrDefaultAsync(x=>x.Id == id);
            return res;
        }

        public async Task<IEnumerable<RatingReview>> GetReviewsByCustomerIdAsync(Guid customerId)
        {
            var res = await _db.RatingReviews.Include(x=>x.Customer).Include(x=>x.Washer).Where(x=>x.CustId==customerId).ToListAsync();
            return res;
        }
        public async Task<RatingReview> CreateReviewAsync(RatingReview obj)
        {
            await _db.RatingReviews.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<RatingReview> UpdateReviewAsync(Guid id , RatingReview obj)
        {
            var res = await _db.RatingReviews.FirstOrDefaultAsync(x=>x.Id == id);
            if(res==null)
                return null;
            res.Rating = obj.Rating;
            res.Review = obj.Review;
            await _db.SaveChangesAsync();
            return obj;
        }
        public async Task<RatingReview> DeleteReviewAsync(Guid id)
        {
            var res = await _db.RatingReviews.FirstOrDefaultAsync(x=>x.Id== id);
            if(res==null)
                return null;
            _db.RatingReviews.Remove(res);
            await _db.SaveChangesAsync();
            return res;
        }
    }
}