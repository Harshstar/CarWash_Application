using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.DTO.UpdateDtos
{
    public class UpdateAddressDto
    {
        [Required]
        public int DoorNumber { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public string Landmark { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Pincode { get; set; }
    }
}