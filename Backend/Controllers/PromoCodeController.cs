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
    public class PromoCodeController : ControllerBase
    {
        private readonly IPromoCode _rr;
        private readonly IMapper _mapper;
        public PromoCodeController(IPromoCode rr , IMapper mapper)
        {
            _rr=rr;
            _mapper = mapper;
        }
        [HttpGet("GetAllPromoCode")]
        [Authorize(Roles = "Customer,Admin")] 
        public async Task<IActionResult> GetAllPromoCodesAsync()
        {
            var res = await _rr.GetAllPromoCodesAsync();
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<List<PromoCodeDto >>(res);
            return Ok(mapp);
        }

        [HttpGet("Get-Package-By-Id/{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetPromoCodeByIdAsync([FromRoute] Guid id)
        {
            var res = await _rr.GetPromoCodeByIdAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<PromoCodeDto>(res);
            return Ok(mapp);
        }

        [HttpPost("add-promocode")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePromoCodeAsync([FromBody] AddPromoCodeDto obj)
        {
            var region = _mapper.Map<PromoCode>(obj);
            var res = await _rr.CreatePromoCodeAsync(region);
            var mapp = _mapper.Map<AddPromoCodeDto>(res);
            return Ok(mapp);
        }

        [HttpDelete("delete-prmocode/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePromoCodeAsync([FromRoute] Guid id)
        {
            var res = await _rr.DeletePromoCodeAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<AddPromoCodeDto>(res);
            return Ok(mapp);
        }
    }
}