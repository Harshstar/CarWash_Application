using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace carwash.Models.Domain
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; } //guid

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

        public Guid? UserId {get;set;}

        [ForeignKey("UserId")]
        public User User {get;set;}

    }
    
}