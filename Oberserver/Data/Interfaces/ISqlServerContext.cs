using Microsoft.Data.SqlClient;

namespace Oberserver.Data.Interfaces
{
    public interface ISqlServerContext : IDisposable
    {
        public Task<SqlConnection> GetConnection();
    }
}