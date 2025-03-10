using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace carwash.Models.Domain
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } 

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        public string Role {get;set;}
        
        
        // Navigation properties
        public  ICollection<Car>? Cars { get; set; }
        public ICollection<WashRequest>? WashRequests { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<RatingReview>? RatingReviews { get; set; }
    }
}