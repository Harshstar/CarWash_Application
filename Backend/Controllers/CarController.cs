using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using carwash.Models.Domain;
using carwash.Models.DTO;
using carwash.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace carwash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICar _rr;
        private readonly IMapper _mapper;
        public CarController(ICar rr , IMapper mapper)
        {
            _rr=rr;
            _mapper = mapper;
        }

        [HttpGet("GetAllCars")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetAlLCars()
        {
            var res = await _rr.GetAllCarsAsync();
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<List<CarDto>>(res);

            return Ok(mapp);
        }

        [HttpGet("Get-Car-By-Id/{id}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetCarById([FromRoute] Guid id)
        {
            var res = await _rr.GetCarByIdAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<CarDto>(res);
            return Ok(mapp);
        }

        [HttpGet("Get-Car-By-CustId/{customerId}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetCarsByCustomerIdAsync([FromRoute] Guid customerId)
        {
            var res = await _rr.GetCarsByCustomerIdAsync(customerId);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<List<CarDto>>(res);
            return Ok(mapp);
        }



        [HttpPost("add-car")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> AddCarAsync([FromBody] AddCarDto obj)
        {
            var region = _mapper.Map<Car>(obj);
            var res = await _rr.AddCarAsync(region);
            var mapp = _mapper.Map<AddCarDto>(res);
            return Ok(mapp);
        }
        [HttpPut("update-car/{id}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> UpdateCarAsync([FromRoute] Guid id, [FromBody] AddCarDto obj)
        {
            var region = _mapper.Map<Car>(obj);
            var res = await _rr.UpdateCarAsync(id,region);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<AddCarDto>(region);
            return Ok(mapp);
        }

        [HttpDelete("delete-car/{carId}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> DeleteCarAsync([FromRoute] Guid carId)
        {
            var res = await _rr.DeleteCarAsync(carId);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<CarDto>(res);
            return Ok(mapp);
        }
    }
}
