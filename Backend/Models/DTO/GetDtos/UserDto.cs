using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace carwash.Models.DTO
{
    public class UserDto
    {
        [Required]
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
        public string Role { get; set; }
        public ICollection<AddCarDto>? Cars {get;set;}
    }
}