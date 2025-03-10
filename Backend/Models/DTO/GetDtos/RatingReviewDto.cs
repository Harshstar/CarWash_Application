using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.DTO;

namespace Backend.Models.DTO.GetDtos
{
    public class RatingReviewDto
    {
        public Guid Id {get;set;}
        public Guid WashReqId { get; set; }
        public Guid WasherId { get; set; }
        public Guid CustId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}