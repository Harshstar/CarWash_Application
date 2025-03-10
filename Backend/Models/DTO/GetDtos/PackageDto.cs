using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.DTO.GetDtos
{
    public class PackageDto
    {
        public Guid Id {get;set;}       
        public string PackName { get; set; }             
        public int PackPrice { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
    }
}