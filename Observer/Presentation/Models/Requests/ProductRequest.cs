using Observer.Presentation.Models.Responses;

namespace Observer.Presentation.Models.Requests
{
    public record ProductRequest
    {
        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public int Stock { get; private set; }
        public DateTime ProductionBatchDate { get; private set; }

        public ProductRequest(string name, string description, int stock, DateTime prodBatchDate, string? category)
        {
            Name = name;
            Stock = stock;
            Description = description;
            ProductionBatchDate = prodBatchDate;
            Category = category is null ? "None" : category;
        }

        public ResponseEnvelope IsValid()
        {
            return null!;
        }
    }
}
