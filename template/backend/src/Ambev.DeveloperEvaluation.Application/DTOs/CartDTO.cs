using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class CartDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "The Date is required")]
        [JsonPropertyName("date")]
        public required string CreatedAt { get; set; }

        [Required(ErrorMessage = "The Items are required")]
        [JsonPropertyName("products")]
        public required List<CartItemDTO> CartItems { get; set; }
    }
}
