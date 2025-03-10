using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace carwash.Models.DTO
{
    public class AddPackageDto
    {
        [Required]
        public string PackName { get; set; }       
        [Required]
        public int PackPrice { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Duration { get; set; }
    }
}