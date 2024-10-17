using Oberserver.Data.Entities;

namespace Oberserver.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> InsertUser(Users userData);
        public Task<bool> SelectUser(int userId);
        public Task<bool> UpdateUser(int userId, Users userData);
        public Task<bool> DeleteUser(int userId);
    }
}
