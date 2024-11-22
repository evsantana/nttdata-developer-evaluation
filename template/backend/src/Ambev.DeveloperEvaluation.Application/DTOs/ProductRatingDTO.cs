using System.ComponentModel;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class ProductRatingDTO
    {
        [DisplayName("Rate")]
        public double Rate { get; set; }

        [DisplayName("Count")]
        public int Count { get; set; }

        public ProductRatingDTO(double rate, int count)
        {
            Rate = rate;
            Count = count;
        }
    }
}
