using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace carwash.Models.Domain
{
    public class Car
    {
        [Key]
        
        public Guid Id { get; set; } 

        [Required]    
        public Guid CustId { get; set; } 

        [Required]
        public string Number { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string Model { get; set; }

        // Navigation property
    
        [ForeignKey("CustId")]
        public User Customer { get; set; }

        public ICollection<WashRequest> WashRequests { get; set;}
    }

}