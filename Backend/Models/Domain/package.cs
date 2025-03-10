using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace carwash.Models.Domain
{
    public class Package
    {
        [Key]
        
        public Guid Id { get; set; } //guid

        [Required]
        public string PackName { get; set; }

        [Required]
        public int PackPrice { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Duration { get; set; }

        public ICollection<WashRequest> WashRequests { get; set; } 
    }

}
