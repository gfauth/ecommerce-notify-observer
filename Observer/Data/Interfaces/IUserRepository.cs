using Observer.Data.Entities;
using Observer.Presentation.Models.Responses;

namespace Observer.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task<UsersEnvelope> InsertUser(Users userData);
        public Task<Users> SelectUser(int userId);
        public Task<bool> UpdateUser(Users userData);
        public Task<bool> DeleteUser(int userId);
    }
}
