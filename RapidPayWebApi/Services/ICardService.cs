using RapidPayWebApi.Models;
using RapidPayWebApi.Models.Dto;

namespace RapidPayWebApi.Services
{
    public interface ICardService
    {
        Task<Card> CreateCardAsync();
        Task PayAsync(Card card, decimal amount);
        Card GetCard(string cardNumber);       
        Task<decimal> GetCardBalanceAsync(string cardNumber);
    }
}
