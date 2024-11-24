using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class ProductRatingDTO
    {
        [JsonPropertyName("Rate")]
        public double Rate { get; set; }

        [JsonPropertyName("Count")]
        public int Count { get; set; }

        public ProductRatingDTO(double rate, int count)
        {
            Rate = rate;
            Count = count;
        }
    }
}
