using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities
{

    /// <summary>
    /// Represents a product in the system
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// The class has access sealed to isolate it in the domain model.
    /// </summary>
    public sealed class Product : BaseEntity
    {

        #region Constructors
        public Product(string title, decimal price, string description, string category, string image, double rating, int count)
        {
            Validation(title, price, description, category, image);
            Rating = rating;
            Count = count;

        }

        public Product(Guid id, string title, decimal price, string description, string category, string image, double rating, int count)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(id.ToString()), "Invalid ID value.");
            Validation(title, price, description, category, image);
            Id = id;
            Rating = rating;
            Count = count;

        }
        #endregion

        #region Properties
        private string _category;

        public string Title { get; private set; } = string.Empty;

        public decimal Price { get; private set; }

        public string Description { get; private set; } = string.Empty;

        public string Category
        {
            get => _category;
            private set
            {
                //Format: remove extra spaces, convert to lowercase, and replace spaces with hyphens
                _category = value.Trim().ToLower().Replace(" ", "-");
            }
        }

        public string? Image { get; private set; } = string.Empty;

        public double Rating { get; private set; }
        public int Count { get; private set; }

        //public ProductRating Rating { get; set; }
        #endregion


        public void Update(string title, decimal price, string description, string category, string image, double rating, int count)
        {
            Validation(title, price, description, category, image);
            Rating = rating;
            Count = count;
        }

        private void Validation(string title, decimal price, string description, string category, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(title), "Title is required.");
            DomainExceptionValidation.When(title.Length < 2, "Title must be at least 2 characters long.");
            DomainExceptionValidation.When(title.Length > 50, "Title cannot be longer than 50 characters.");

            DomainExceptionValidation.When(price <= 0, "The 'price' field must be a positive number.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Description is required.");
            DomainExceptionValidation.When(description.Length < 2, "Description must be at least 2 characters long.");
            DomainExceptionValidation.When(description.Length > 250, "Description cannot be longer than 50 characters.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(category), "Category is required.");
            DomainExceptionValidation.When(category.Length < 2, "Category must be at least 2 characters long.");
            DomainExceptionValidation.When(category.Length > 250, "Category cannot be longer than 50 characters.");

            DomainExceptionValidation.When(category.Length > 250, "Image cannot be longer than 250 characters.");

            Title = title;
            Price = price;
            Description = description;
            Category = category;
            Image = image;
        }

    }
}
