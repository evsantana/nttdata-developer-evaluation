using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class SaleItemDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Product ID is required")]
        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is required")]
        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("discount")]
        [JsonIgnore]
        public decimal Discount { get; set; }

        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Field isCancelled is required")]
        [JsonPropertyName("isCancelled")]
        public bool IsCancelled { get; set; } = false;

        [JsonIgnore]
        public Guid SaleId { get; set; }
    }
}
