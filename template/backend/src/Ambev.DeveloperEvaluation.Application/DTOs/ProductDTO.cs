using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class ProductDTO
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Title is required")]
        [MinLength(3)]
        [MaxLength(50)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Price is required")]
        [Column(TypeName = "decimal(18,2")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Description is required")]
        [MinLength(5)]
        [MaxLength(250)]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Category is required")]
        [MinLength(2)]
        [MaxLength(250)]
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [MaxLength(250)]
        [JsonPropertyName("image")]
        public string Image { get; set; }

        //[JsonPropertyName("date created")]
        //public string CreatedAt { get; set; }

        [JsonPropertyName("rating")]
        public ProductRatingDTO Rating { get; set; }
    }
}
