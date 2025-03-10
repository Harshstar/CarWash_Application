using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Models.DTO.GetDtos;
using Backend.Models.DTO.UpdateDtos;
using carwash.Models.Domain;
using carwash.Models.DTO;
using carwash.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace carwash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddress _rr;
        private readonly IMapper _mapper;
        public AddressController(IAddress rr , IMapper mapper)
        {
            _rr=rr;
            _mapper = mapper;
        }

        [HttpGet("GetAllAddress")]
        [Authorize(Roles = "Customer,Admin,Washer")]
        public async Task<IActionResult> GetAllAddressAsync()
        {
            var res = await _rr.GetAllAddressAsync();
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<List<AddressDto>>(res);
            return Ok(mapp);
        }

        [HttpGet("Get-Address-By-Id/{id}")]
        [Authorize(Roles = "Customer,Admin,Washer")]
        public async Task<IActionResult> GetAddressByIdAsync([FromRoute] Guid id)
        {
            var res = await _rr.GetAddressByIdAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<AddressDto>(res);
            return Ok(mapp);
        }

        [HttpGet("Get-Address-By-City/{city}")]
        [Authorize(Roles = "Customer,Admin,Washer")]
        public async Task<IActionResult> GetAddressByCityAsync([FromRoute] string city)
        {
            var res = await _rr.GetAddressByCityAsync(city);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<IEnumerable<AddressDto>>(res);
            return Ok(mapp);
        }

        [HttpGet("Get-Address-By-userId/{userId}")]
        [Authorize(Roles = "Customer,Admin,Washer")]
        public async Task<IActionResult> GetAddressByUserIdAsync([FromRoute] Guid userId)
        {
            var res = await _rr.GetAddressByUserIdAsync(userId);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<AddressDto>(res);
            return Ok(mapp);
        }

    

        [HttpPost("add-address")]
        [Authorize(Roles = "Customer,Admin,Washer")]
        public async Task<IActionResult> CreateAddressAsync([FromBody] AddAddressDto obj)
        {
            var region = _mapper.Map<Address>(obj);
            var res = await _rr.CreateAddressAsync(region);
            var mapp = _mapper.Map<AddAddressDto>(res);
            return Ok(mapp);
        }

        [HttpPut("update-address/{id}")]
        [Authorize(Roles = "Customer,Admin,Washer")]
        public async Task<IActionResult> UpdateAddressAsync([FromRoute] Guid id, [FromBody] AddAddressDto obj)
        { 
            var region = _mapper.Map<Address>(obj);
            var res = await _rr.UpdateAddressAsync(id,region);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<UpdateAddressDto>(region);
            return Ok(mapp);
        }

        [HttpPut("update-addressByUserId/{userId}")]
        [Authorize(Roles = "Customer,Admin,Washer")]
        public async Task<IActionResult> UpdateAddressByUserIdAsync([FromRoute] Guid userId, [FromBody] UpdateAddressDto obj)
        { 
            var region = _mapper.Map<Address>(obj);
            var res = await _rr.UpdateAddressByUserIdAsync(userId,region);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<UpdateAddressDto>(region);
            return Ok(mapp);
        }

        [HttpDelete("delete-address/{id}")]
        [Authorize(Roles = "Customer,Admin,Washer")]
        public async Task<IActionResult> DeleteAddressAsync([FromRoute] Guid id)
        {
            var res = await _rr.DeleteAddressAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<AddAddressDto>(res);
            return Ok(mapp);
        }
    }
}