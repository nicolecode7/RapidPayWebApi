using Microsoft.EntityFrameworkCore;
using RapidPayWebApi.Data;
using RapidPayWebApi.Models;
using RapidPayWebApi.Models.Dto;

namespace RapidPayWebApi.Services
{
    public class CardService : ICardService
    {
        private decimal _lastFee;
        private readonly ApplicationDbContext _db;
        private readonly IUfeService _ufeService;
        private static readonly object _lock = new object();
        private readonly List<Card> _cards = new List<Card>();
        public CardService(ApplicationDbContext db, IUfeService ufeService)
        {
            _db = db;
            _ufeService = ufeService;
            _lastFee = 0; // Initial fee
        }

        public async Task<Card> CreateCardAsync()
        {
            var card = new Card { CardNumber = GenerateRandomCardNumber(), Balance = 100 };
           
            _db.Cards.Add(card);
            await _db.SaveChangesAsync();
            return card;
        }

        public async Task PayAsync(Card card, decimal amount)
        {           
            card.Balance -= amount;
            _db.Cards.Update(card);
            await _db.SaveChangesAsync();

            // Add a record to PaymentRequests table
            var paymentRequest = new PaymentRequest
            {
                Card = card,
                Amount = amount,
                PaymentDate = DateTime.UtcNow // Use UTC time for consistency
            };

            _db.PaymentRequests.Add(paymentRequest);
            await _db.SaveChangesAsync();
        }
      
        public async Task<decimal> GetCardBalanceAsync(string cardNumber)
        {          
            var card = await _db.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
            return card?.Balance ?? 0;
        }

        private string GenerateRandomCardNumber()
        {      
            lock (_lock)
            {
                Random random = new Random();
                long cardNumber = (long)(random.NextDouble() * (999999999999999 - 100000000000000) + 100000000000000);
                return cardNumber.ToString();
            }
            
        }       

        public Card GetCard(string cardNumber)
        {                           
            return _db.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);            
        }       
    }
}
