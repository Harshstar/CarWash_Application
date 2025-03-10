using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.Domain;

namespace carwash.Models.DTO
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public Guid CustId { get; set; }
        public string Number { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        
    }
}