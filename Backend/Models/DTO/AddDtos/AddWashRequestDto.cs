using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace carwash.Models.DTO
{
    public class AddWashRequestDto
    {
        public Guid CustId { get; set; }
        public Guid CarId { get; set; }
        public Guid WasherId { get; set; }
        public Guid PackageId { get; set; }
        public Guid AddressId { get; set; }
        public DateTime OrderedDate { get; set; }
        public string Notes { get; set; }
        public string WashType { get; set; }
        public DateTime DeliveryDate {get;set;}
        public DateTime PickupDate {get;set;}
    }
}