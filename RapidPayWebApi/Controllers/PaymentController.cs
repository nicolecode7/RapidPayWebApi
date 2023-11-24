using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidPayWebApi.Models;
using RapidPayWebApi.Models.Dto;
using RapidPayWebApi.Services;

namespace RapidPayWebApi.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires authentication for all actions
    public class PaymentController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IUfeService _ufeService;

        public PaymentController(ICardService cardService, IUfeService ufeService)
        {
            _cardService = cardService;
            _ufeService = ufeService;
        }
   
        [HttpPost("CreateCard")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Card>> CreateCard()
        {
            var card = await _cardService.CreateCardAsync();

            //return Ok(card);
            return Created(nameof(CreateCard), card);

        }

        [HttpPost("Pay")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Pay([FromBody] PaymentRequest paymentRequest)
        {
            var card = _cardService.GetCard(paymentRequest.Card.CardNumber);
            
            if (card == null)
            {
                return BadRequest("Invalid card number.");
            }

            decimal paymentFee = _ufeService.GetPaymentFee();
            decimal totalAmountPay = paymentRequest.Amount + paymentFee;

            if (card.Balance > totalAmountPay)
            {
                await _cardService.PayAsync(card, totalAmountPay);
                return Ok($"Payment successful. Remaining balance: {card.Balance.ToString("C2")}");
            }
            else
            {
                return BadRequest("Insufficient funds.");                
            }            
        }

        [HttpGet("GetBalance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<decimal>> GetBalance(string cardNumber)
        {            
            var balance = await _cardService.GetCardBalanceAsync(cardNumber);
            return Ok(balance);
        }        
    }
}
