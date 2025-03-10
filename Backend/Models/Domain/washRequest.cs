using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace carwash.Models.Domain
{
    public class WashRequest
    {
        [Key]
        public Guid Id { get; set; } //guid
        [Required]
        public Guid CustId { get; set; } //guid

        [Required]
        public Guid CarId { get; set; } //guid

        [Required]
        public Guid PackageId { get; set; } //guid

        [Required]
        public Guid AddressId {get;set;} //guid

        public string Notes { get; set; }

        [Required]
        public Guid WasherId { get; set; } //guid
        [Required]
        public DateTime OrderedDate { get; set; }
        [Required]
        public DateTime PickupDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public string WashType { get; set; }

        // Navigation properties
        [ForeignKey("CustId")]
        public User Customer { get; set; } 

        [ForeignKey("CarId")]
        public Car Car { get; set; }


        [ForeignKey("PackageId")]
        public Package PackageNavigation { get; set; }

        [ForeignKey("WasherId")]
        public User Washer { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }
    }

}