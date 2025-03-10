using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Services;
using carwash.Models.Domain;
using carwash.Models.DTO;
using carwash.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace carwash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WashReqController : ControllerBase
    {
        private readonly IWashRequest _rr;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public WashReqController(IWashRequest rr , IMapper mapper, IEmailService emailservice)
        {
            _rr=rr;
            _mapper = mapper;
            _emailService = emailservice;
        }
        [HttpGet("GetAllWashRequestAsync")]
        [Authorize(Roles = "Admin,Washer")]
        public async Task<IActionResult> GetAllWashRequestAsync()
        {
            var res = await _rr.GetAllWashRequestAsync();
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<List<WashRequestDto>>(res);
            return Ok(mapp);
        }

        [HttpGet("Get-WashRequest-By-Id/{id}")]
        [Authorize(Roles = "Admin,Washer")]
        public async Task<IActionResult> GetWashRequestByIdAsync([FromRoute] Guid id)
        {
            var res = await _rr.GetWashRequestByIdAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<WashRequestDto>(res);
            return Ok(mapp);
        }


        [HttpGet("Get-LatestWashRequest-By-CustId/{customerId}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetLatestWashRequestByCustIdAsync([FromRoute] Guid customerId)
        {
            var res = await _rr.GetLatestWashRequestByCustIdAsync(customerId);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<WashRequestDto>(res);
            return Ok(mapp);
        }

        [HttpGet("GetCustomerWashRequests/{customerId}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetCustomerWashRequests([FromRoute] Guid customerId)
        {
            var res = await _rr.GetCustomerWashRequests(customerId);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<List<WashRequestDto>>(res);
            return Ok(mapp);
        }


        [HttpGet("Get-WashRequest-By-washerId/{washerId}")]
        [Authorize(Roles = "Admin,Washer")]
        public async Task<IActionResult> GetWashRequestBywasherIdAsync([FromRoute] Guid washerId)
        {
            var res = await _rr.GetWashRequestBywasherIdAsync(washerId);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<List<WashRequestDto>>(res);
            return Ok(mapp);
        }



        [HttpPost("add-washrequest")]
        [Authorize(Roles = "Admin,Customer,Washer")]
        public async Task<IActionResult> CreateWashRequestAsync([FromBody] AddWashRequestDto obj)
        {
            var region = _mapper.Map<WashRequest>(obj);
            var res = await _rr.CreateWashRequestAsync(region);

            var washer=await _rr.WasherDetails(obj.WasherId);
            var emailSubject="New Wash Request";
            var emailBody="You just received a new wash request scheduled. Please open the website for more details.";
            await _emailService.SendEmailAsync(washer.Email,emailSubject, emailBody);

            var mapp = _mapper.Map<AddWashRequestDto>(res);
            return Ok(mapp);
        }
        [HttpGet("IsWasherAvailable/{washerId}/{pickupDate}")]
        public async Task<bool> IsWasherAvailable([FromRoute] Guid washerId,DateTime pickupDate)
        {
            var res = await _rr.IsWasherAvailable(washerId,pickupDate);
            if(res==false)
                return false;
            return true;
        }
        [HttpPut("update-WashRequest/{id}")]
        [Authorize(Roles = "Admin,Customer,Washer")]
        public async Task<IActionResult> UpdateWashRequestAsync([FromRoute] Guid id, [FromBody] WashRequestDto obj)
        {
            var region = _mapper.Map<WashRequest>(obj);
            var res = await _rr.UpdateWashRequestAsync(id,region);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<WashRequestDto>(region);
            return Ok(mapp);
        }

        [HttpDelete("delete-washrequest/{id}")]
        [Authorize(Roles = "Admin,Customer,Washer")]
        public async Task<IActionResult> DeleteWashRequestAsync([FromRoute] Guid id)
        {
            var res = await _rr.DeleteWashRequestAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<WashRequestDto>(res);
            return Ok(mapp);
        }
    }
}