using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace carwash.Models.Domain
{
    public class PromoCode
    {
        [Key]
    
        public Guid Id { get; set; }  //guid

        [Required]
        public string Promo { get; set; }

        [Required]
        public int Discount { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MinVal { get; set; }

        // Navigation property
        public ICollection<Payment> Payments { get; set; }
    }   

}