using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.Domain;

namespace Backend.Models.DTO.GetDtos
{
    public class AddressDto
    {
        public Guid Id {get;set;}
        public int DoorNumber { get; set; }
        public string Street { get; set; }
        public string Area { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public Guid? UserId {get;set;}
        public User User { get; set; }
    }
}