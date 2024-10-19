using Dapper;
using Microsoft.Data.SqlClient;
using Oberserver.Constants;
using Oberserver.Data.Entities;
using Oberserver.Data.Interfaces;
using Oberserver.Presentation.Models.Responses;

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
            await using SqlConnection connection = await _sqlServerContext.GetConnection();

            try
            {
                var query = QueryData.DeleteUsers;
                var response = await connection.ExecuteAsync(query, new { userId });

                return response > 0;
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                    return false;

                throw;
            }
            finally
            {
                await _sqlServerContext.DisposeAsync();
            }
        }

        public async Task<UsersEnvelope> InsertUser(Users userData)
        {
            await using SqlConnection connection = await _sqlServerContext.GetConnection();

            try
            {
                var query = QueryData.InsertUsers;
                var response = await connection.QueryAsync<int>(query, userData);


                return new UsersEnvelope(response.FirstOrDefault(), userData);
            }
            finally
            {
                await _sqlServerContext.DisposeAsync();
            }
        }

        public async Task<Users> SelectUser(int userId)
        {
            await using SqlConnection connection = await _sqlServerContext.GetConnection();

            try
            {
                var query = QueryData.SelectOneUsers;
                var response = await connection.QuerySingleAsync<Users>(query, new { userId });

                return response;
            }
            catch(Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                    return null!;

                throw;
            }
            finally
            {
                await _sqlServerContext.DisposeAsync();
            }
        }

        public async Task<bool> UpdateUser(Users userData)
        {
            await using SqlConnection connection = await _sqlServerContext.GetConnection();
            
            try
            {
                var query = QueryData.UpdateUsers;
                var response = await connection.ExecuteAsync(query, userData);

                return response > 0;
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                    return false;

                throw;
            }
            finally
            {
                await _sqlServerContext.DisposeAsync();
            }
        }
    }
}
