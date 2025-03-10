using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace carwash.Models.DTO
{
    public class AddRatingReviewDto
    {
        [Required]
        public Guid WashReqId { get; set; }
        [Required]
        public Guid WasherId { get; set; }
        [Required]
        public Guid CustId { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Review { get; set; }
    }
}