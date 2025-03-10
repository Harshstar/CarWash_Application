using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Models.DTO.GetDtos;
using carwash.Models.Domain;
using carwash.Models.DTO;
using carwash.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace carwash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingReviewController : ControllerBase
    {
        private readonly IRatingReview _rr;
        private readonly IMapper _mapper;
        public RatingReviewController(IRatingReview rr , IMapper mapper)
        {
            _rr=rr;
            _mapper = mapper;
        }

        [HttpGet("GetAllRatings")]
        public async Task<IActionResult> GetAlLReviews()
        {
            var res = await _rr.GetAllReviewsAsync();
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<List<RatingReviewDto>>(res);
            return Ok(mapp);
        }

        [HttpGet("Get-Rating-By-Id/{id}")]
        public async Task<IActionResult> GetReviewByIdAsync([FromRoute] Guid id)
        {
            var res = await _rr.GetReviewByIdAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<RatingReviewDto>(res);
            return Ok(mapp);
        }
        
        [HttpPost("add-review")]
        public async Task<IActionResult> CreateReviewAsync([FromBody] AddRatingReviewDto obj)
        {
            var region = _mapper.Map<RatingReview>(obj);
            var res = await _rr.CreateReviewAsync(region);
            var mapp = _mapper.Map<AddRatingReviewDto>(res);
            return Ok(mapp);
        }

        [HttpPut("update-review/{id}")]
        public async Task<IActionResult> UpdateReviewAsync([FromRoute] Guid id, [FromBody] AddRatingReviewDto obj)
        {
            var region = _mapper.Map<RatingReview>(obj);
            var res = await _rr.UpdateReviewAsync(id,region);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<AddRatingReviewDto>(region);
            return Ok(mapp);
        }

        [HttpDelete("delete-review/{id}")]
        public async Task<IActionResult> DeleteReviewAsync([FromRoute] Guid id)
        {
            var res = await _rr.DeleteReviewAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<AddRatingReviewDto>(res);
            return Ok(mapp);
        }
    }
}