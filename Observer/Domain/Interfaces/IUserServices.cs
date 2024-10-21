using Observer.Presentation.Models.Requests;
using Observer.Presentation.Models.Responses;

namespace Observer.Domain.Interfaces
{
    public interface IUserServices
    {
        public Task<UserResponse> RetrieveUser(int userId);
        public Task<UserResponse> CreateUser(UserRequest user);
        public Task<UserResponse> UpdateUser(int userId, UserRequest user);
        public Task<UserResponse> DeleteUser(int userId);
    }
}
