using Dapper;
using Microsoft.Data.SqlClient;
using Oberserver.Constants;
using Oberserver.Data.Entities;
using Oberserver.Data.Interfaces;

namespace Oberserver.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlServerContext _sqlServerContext;

        public UserRepository(ISqlServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertUser(Users userData)
        {
            try
            {
                await using SqlConnection connection = await _sqlServerContext.GetConnection();

                var query = QueryData.InsertUsers;

                var response = await connection.ExecuteAsync(query, userData);

                return response > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> SelectUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUser(int userId, Users userData)
        {
            throw new NotImplementedException();
        }
    }
}
