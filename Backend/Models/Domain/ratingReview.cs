using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace carwash.Models.Domain
{
    public class RatingReview
    {
        [Key]
        
        public Guid Id { get; set; }//guid

        [Required]
        public Guid WashReqId { get; set; } //guid
        [Required]
        public Guid WasherId { get; set; } //guid

        [Required]
        public Guid CustId { get; set; } //guid

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Review { get; set; }

        // Navigation properties
        [ForeignKey("WasherId")]
        public User Washer { get; set; }
        
        [ForeignKey("WashReqId")]
        public WashRequest WashRequest { get; set; }

        [ForeignKey("CustId")]
        public User Customer { get; set; }
    }

}