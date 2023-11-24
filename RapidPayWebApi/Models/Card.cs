using System.ComponentModel.DataAnnotations;

namespace RapidPayWebApi.Models
{
    public class Card
    {
        [Key]
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
