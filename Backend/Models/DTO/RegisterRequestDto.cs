using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace carwash.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username {get;set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        public string[] Roles {get;set;}

        public string Name { get; set; }
 
        [Required]
        [Phone]
        public string MobileNumber { get; set; }
    }
}
