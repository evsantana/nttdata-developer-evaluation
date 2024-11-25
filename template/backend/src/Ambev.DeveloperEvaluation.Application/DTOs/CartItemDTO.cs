using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class CartItemDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Product ID is required")]
        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public Guid CartId { get; set; }
    }
}
