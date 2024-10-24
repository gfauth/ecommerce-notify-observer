using Observer.Data.Entities;

namespace Observer.Presentation.Models.Responses
{
    /// <summary>
    /// User data response envelope.
    /// </summary>
    public record ProductsEnvelope
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public int Stock { get; private set; }
        public DateTime ProductionBatchDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// Constructor for product data response envelope.
        /// </summary>
        /// <param name="product">Object Users.</param>
        public ProductsEnvelope(Products product)
        {
            Id = product.Id;
            Name = product.Name;
            Category = product.Category;
            ProductionBatchDate = product.ProductionBatchDate;
            Description = product.Description;
            Stock = product.Stock;
            CreatedAt = product.CreatedAt;
            UpdatedAt = product.UpdatedAt;
        }

        /// <summary>
        /// Constructor for product data response envelope with id before creation.
        /// </summary>
        /// <param name="productId">New product identification.</param>
        /// <param name="product">Object Products.</param>
        public ProductsEnvelope(int productId, Products product)
        {
            Id = productId;
            Name = product.Name;
            Category = product.Category;
            ProductionBatchDate = product.ProductionBatchDate;
            Description = product.Description;
            Stock = product.Stock;
            CreatedAt = product.CreatedAt;
            UpdatedAt = product.UpdatedAt;
        }
    }
}
