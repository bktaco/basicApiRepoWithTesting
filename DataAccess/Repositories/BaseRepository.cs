using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly IConfiguration _config;
        private readonly ConnectionStringData _connectionString;

        public BaseRepository(IConfiguration config, ConnectionStringData connectionString)
        {
            _config = config;
            _connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _config.GetConnectionString(_connectionString.MySqlConnectionName);

        }
    }
}
