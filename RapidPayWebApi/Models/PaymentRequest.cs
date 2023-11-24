using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPayWebApi.Models
{
  
    public class PaymentRequest
    {
        public int Id { get; set; }
        public Card Card { get; set; }        
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
