using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Models.DTO.GetDtos;
using carwash.Models.Domain;
using carwash.Models.DTO;
using carwash.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace carwash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackageController : ControllerBase
    {
        private readonly IPackage _rr;
        private readonly IMapper _mapper;
        public PackageController(IPackage rr , IMapper mapper)
        {
            _rr=rr;
            _mapper = mapper;
        }
        [HttpGet("GetAllPackage")]
        [Authorize(Roles = "Customer,Admin,Washer")]
        public async Task<IActionResult> GetAllPackagesAsync()
        {
            var res = await _rr.GetAllPackagesAsync();
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<List<PackageDto>>(res);
            return Ok(mapp);
        }

        [HttpGet("Get-Package-By-Id/{id}")]
        [Authorize(Roles = "Customer,Admin,Washer")]
        public async Task<IActionResult> GetPackageByIdAsync([FromRoute] Guid id)
        {
            var res = await _rr.GetPackageByIdAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<PackageDto>(res);
            return Ok(mapp);
        }

        [HttpPost("add-package")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePackageAsync([FromBody] AddPackageDto obj)
        {
            var region = _mapper.Map<Package>(obj);
            var res = await _rr.CreatePackageAsync(region);
            var mapp = _mapper.Map<AddPackageDto>(res);
            return Ok(mapp);
        }

        [HttpPut("update-package/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePackageAsync([FromRoute] Guid id, [FromBody] AddPackageDto obj)
        {
            var region = _mapper.Map<Package>(obj);
            var res = await _rr.UpdatePackageAsync(id,region);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<AddPackageDto>(region);
            return Ok(mapp);
        }

        [HttpDelete("delete-package/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePackageAsync([FromRoute] Guid id)
        {
            var res = await _rr.DeletePackageAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<AddPackageDto>(res);
            return Ok(mapp);
        }
    }
}