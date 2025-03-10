using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace carwash.Models.Domain
{
    public class Payment
    {
        [Key]
        
        public Guid Id { get; set; } 

        [Required]
        public int Amount { get; set; }

        [Required]
        public string Method { get; set; }

        [Required]
        public Guid CustId { get; set; } 

        [Required]
        public DateTime PaymentTime {get;set;}

        [Required]
        public Guid WasherId { get; set; } 


        public Guid? PromoId { get; set; } 
        
        [Required]
        public Guid WashRequestId {get;set;} //guid

        // Navigation properties
        [ForeignKey("CustId")]
        public User Customer { get; set; }

        [ForeignKey("WasherId")]
        public User Washer { get; set; }

        [ForeignKey("PromoId")]
        public PromoCode PromoCode { get; set; }
        [ForeignKey("WashRequestId")]
        public WashRequest WashRequest { get; set; }

    }   

}