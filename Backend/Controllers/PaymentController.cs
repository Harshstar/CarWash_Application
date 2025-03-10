using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Models.DTO;
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
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _rr;
        private readonly IMapper _mapper;
        public PaymentController(IPayment rr , IMapper mapper)
        {
            _rr=rr;
            _mapper = mapper;
        }
        [HttpGet("GetAllPayments")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetAllPaymentAsync()
        {
            var res = await _rr.GetAllPaymentAsync();
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<List<PaymentDto>>(res);
            return Ok(mapp);
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] PaymentIntentDto paymentIntentDto)
        {
            try
            {
                Console.WriteLine($"Received Payment Data: Amount = {paymentIntentDto.Amount}, Currency = {paymentIntentDto.Currency}");
                var options = new Stripe.Checkout.SessionCreateOptions  // Fully Qualified Name
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
                    {
                        new Stripe.Checkout.SessionLineItemOptions
                        {
                            PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                            {
                                Currency = paymentIntentDto.Currency,
                                UnitAmount = paymentIntentDto.Amount,
                                ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = "Car Wash Service"
                                }
                            },
                            Quantity = 1
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = "http://localhost:4200/customer",
                    // CancelUrl = "http://localhost:4200/cancel"
                };

                var service = new Stripe.Checkout.SessionService();
                var session = await service.CreateAsync(options);

                return Ok(new { sessionId = session.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        


        [HttpPost("add-payment")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> CreatePayemntAsync([FromBody] AddPaymentDto obj)
        {
            var region = _mapper.Map<Payment>(obj);
            var res = await _rr.CreatePayemntAsync(region);
            var mapp = _mapper.Map<AddPaymentDto>(res);
            return Ok(mapp);
        }

        [HttpDelete("delete-payment/{id}")]
        public async Task<IActionResult> DeletePaymentAsync([FromRoute] Guid id)
        {
            var res = await _rr.DeletePaymentAsync(id);
            if(res==null)
                return NotFound();
            var mapp = _mapper.Map<AddPaymentDto>(res);
            return Ok(mapp);
        }
    }
}