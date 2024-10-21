using Dapper;
using Microsoft.Data.SqlClient;
using Observer.Constants;
using Observer.Data.Entities;
using Observer.Data.Interfaces;
using Observer.Presentation.Logs;
using Observer.Presentation.Models.Responses;
using SingleLog.Interfaces;
using SingleLog.Models;

namespace Observer.Data
{
    internal class UserRepository : IUserRepository
    {
        private readonly ISqlServerContext _sqlServerContext;
        private readonly ISingleLog<LogModel> _singleLog;

        public UserRepository(ISqlServerContext sqlServerContext, ISingleLog<LogModel> singleLog)
        {
            _sqlServerContext = sqlServerContext;
            _singleLog = singleLog;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_DATABASE_DELETE_DATA, sublog);
            sublog.StopwatchStart();

            try
            {
                await using SqlConnection connection = await _sqlServerContext.GetConnection();

                var query = QueryData.DeleteUsers;
                var response = await connection.ExecuteAsync(query, new { userId });

                return response > 0;
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;

                if (ex.Message.Equals("Sequence contains no elements"))
                    return false;

                throw;
            }
            finally
            {
                sublog.StopwatchStop();

                await _sqlServerContext.DisposeAsync();
            }
        }

        public async Task<UsersEnvelope> InsertUser(Users userData)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_DATABASE_CREATE_DATA, sublog);
            sublog.StopwatchStart();

            try
            {
                await using SqlConnection connection = await _sqlServerContext.GetConnection();

                var query = QueryData.InsertUsers;
                var response = await connection.QueryAsync<int>(query, userData);


                return new UsersEnvelope(response.FirstOrDefault(), userData);
            }
            finally
            {
                sublog.StopwatchStop();

                await _sqlServerContext.DisposeAsync();
            }
        }

        public async Task<Users> SelectUser(int userId)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_DATABASE_RETRIEVE_DATA, sublog);
            sublog.StopwatchStart();

            try
            {
                await using SqlConnection connection = await _sqlServerContext.GetConnection();

                var query = QueryData.SelectOneUsers;
                var response = await connection.QuerySingleAsync<Users>(query, new { userId });

                return response;
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                    return null!;

                throw;
            }
            finally
            {
                sublog.StopwatchStop();

                await _sqlServerContext.DisposeAsync();
            }
        }

        public async Task<bool> UpdateUser(Users userData)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_DATABASE_UPDATE_DATA, sublog);
            sublog.StopwatchStart();

            try
            {
                await using SqlConnection connection = await _sqlServerContext.GetConnection();

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
                sublog.StopwatchStop();

                await _sqlServerContext.DisposeAsync();
            }
        }
    }
}
