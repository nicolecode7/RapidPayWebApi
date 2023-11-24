namespace RapidPayWebApi.Services
{
    public class UfeService : IUfeService
    {
        private decimal lastFee = 0.1m; // Initialize with a non-zero value for the first calculation
        private readonly Random random;
        private readonly object lockObject = new object();
        public UfeService()
        {
            random = new Random();
        }       

        public decimal GetPaymentFee()
        {
            lock (lockObject)
            {
                // Generate a random decimal between 0 and 2
                decimal randomDecimal = (decimal)random.NextDouble() * 2;

                // Calculate the new fee by multiplying the last fee with the random decimal
                lastFee = lastFee * randomDecimal;

                return lastFee;
            }
            
        }

      
    }
}
