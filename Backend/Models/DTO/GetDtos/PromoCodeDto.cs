using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.DTO.GetDtos
{
    public class PromoCodeDto
    {
        public Guid Id { get; set;}
        public string Promo { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public int MinVal { get; set; }
    }
}