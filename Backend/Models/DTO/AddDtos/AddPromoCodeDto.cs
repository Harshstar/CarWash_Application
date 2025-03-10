using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace carwash.Models.DTO
{
    public class AddPromoCodeDto
    {
        

        [Required]
        public string Promo { get; set; }

        [Required]
        public int Discount { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MinVal { get; set; }
    }
}