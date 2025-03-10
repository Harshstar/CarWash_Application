using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.DTO.GetDtos
{
    public class PaymentDto
    {
        public Guid Id {get;set;}
        public int Amount { get; set; }
        public string Method { get; set; }
        public DateTime PaymentTime {get;set;}
        public Guid CustId { get; set; }
        public Guid WasherId { get; set; }
        public Guid? PromoId { get; set; }
        public Guid WashRequestId {get;set;}
    }
}