using Observer.Presentation.Models.Requests;
using Observer.Presentation.Models.Responses;

namespace Observer.Domain.Interfaces
{
    public interface IProductServices
    {
        public Task<ResponseEnvelope> RetrieveProduct(int productId);
        public Task<ResponseEnvelope> CreateProduct(ProductRequest product);
        public Task<ResponseEnvelope> UpdateProduct(int productId, ProductRequest product);
        public Task<ResponseEnvelope> DeleteProduct(int productId);
        //public Task<ResponseEnvelope> ListProduct();
    }
}
