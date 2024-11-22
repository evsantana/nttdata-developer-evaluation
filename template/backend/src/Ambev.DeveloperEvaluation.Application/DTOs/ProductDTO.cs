using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class ProductDTO
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Title is required")]
        [MinLength(3)]
        [MaxLength(50)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Price is required")]
        [Column(TypeName = "decimal(18,2")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Description is required")]
        [MinLength(5)]
        [MaxLength(250)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Category is required")]
        [MinLength(2)]
        [MaxLength(250)]
        [DisplayName("Category")]
        public string Category { get; set; }

        [MaxLength(250)]
        [DisplayName("Image")]
        public string Image { get; set; }

        [DisplayName("Rating")]
        public ProductRatingDTO Rating { get; set; }
    }
}
