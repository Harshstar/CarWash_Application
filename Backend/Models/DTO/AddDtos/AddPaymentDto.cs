using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace carwash.Models.DTO
{
    public class AddPaymentDto
    {
        

        [Required]
        public int Amount { get; set; }

        [Required]
        public string Method { get; set; }

        [Required]
        public DateTime PaymentTime {get;set;}

        [Required]
        public Guid CustId { get; set; }

        [Required]
        public Guid WasherId { get; set; }

        public Guid? PromoId { get; set; }
        
        [Required]
        public Guid WashRequestId {get;set;}
    }
}