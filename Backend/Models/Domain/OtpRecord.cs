using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.Domain
{
    public class OtpRecord
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Otp { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}