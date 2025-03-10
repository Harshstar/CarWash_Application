using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Models.DTO.GetDtos;
using Backend.Models.DTO.UpdateDtos;
using carwash.Data;
using carwash.Models.Domain;
using carwash.Models.DTO;
using carwash.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace carwash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _rr;
        private readonly IMapper _mapper;
        public UserController(IUser rr , IMapper mapper)
        {
            _rr=rr;
            _mapper = mapper;
        }
        [HttpGet("GetAllUser")]
        [Authorize(Roles = "Admin,Customer,Washer")]
        public async Task<IActionResult> GetUser()
        {
            var res = await _rr.GetAllUserAsync();
            if(res==null)
                return NotFound();
            
            
                // for(int i=0 ; i<res.Count() ; i++){
                //     if(res[i].Role == "Customer")
                // {
                //     var mapps = _mapper.Map<List<UserDto>>(res);
                //     return Ok(mapps);
                //     //  var mapp = _mapper.Map<List<WasherDto>>(res);
                //     // return Ok(mapp);
                // }
                // var mapp = _mapper.Map<List<WasherDto>>(res);
                // return Ok(mapp);

                // }
            var mapps = _mapper.Map<List<UserDto>>(res);
            return Ok(mapps);
        }

        [HttpGet("Get-User-By-Id/{userId}")]
        [Authorize(Roles = "Admin,Customer,Washer")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId)
        {
            var res = await _rr.GetUserByIdAsync(userId);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<UserDto>(res);
            return Ok(mapp);
        }

        [HttpPost("add-user")]
        [Authorize(Roles = "Admin,Customer,Washer")]
        public async Task<IActionResult> AddUserAsync([FromBody] AddUserDto obj)
        {
            var region = _mapper.Map<User>(obj);
            var res = await _rr.AddUserAsync(region);
            var mapp = _mapper.Map<AddUserDto>(res);
            return Ok(mapp);
        }

        [HttpPut("update-user/{id}")]
        [Authorize(Roles = "Admin,Customer,Washer")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserDto obj)
        {
            var region = _mapper.Map<User>(obj);
            var res = await _rr.UpdateUserAsync(id,region);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<UpdateUserDto>(region);
            return Ok();
        }

        [HttpDelete("delete-user/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
        {
            var res = await _rr.DeleteUserAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<AddUserDto>(res);
            return Ok();
        }
    }
}
