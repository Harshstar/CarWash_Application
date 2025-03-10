using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace carwash.Models.DTO
{
    public class AddCarDto
    {
        
        [Required]
        public Guid CustId { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string Model { get; set; }
        
    }
}