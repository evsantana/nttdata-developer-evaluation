using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class SaleDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Sale Number is required")]
        [JsonPropertyName("saleNumber")]
        public string SaleNumber { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        [JsonPropertyName("useId")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Sale Date is required")]
        [JsonPropertyName("saleDate")]
        public DateTime SaleDate { get; set; }

        [JsonPropertyName("totalAmount")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Branch is required")]
        [JsonPropertyName("branch")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "Canceled status is required")]
        [JsonPropertyName("isCancelled")]
        public bool IsCanceled { get; set; } = false;

        [Required(ErrorMessage = "Items are required")]
        [JsonPropertyName("products")]
        public List<SaleItemDTO> SaleItems { get; set; }
    }
}
