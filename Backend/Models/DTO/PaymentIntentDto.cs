using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.DTO
{
    public class PaymentIntentDto
    {
        public int Amount { get; set; }  // Amount in cents (from frontend)
        public string Currency { get; set; }
    }
}