using MySql.Data.MySqlClient;
using System.Data;

namespace WebTest5.Components.Data
{    public class DataContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            if (_configuration.GetConnectionString("DefaultConnection") == null)
            {
                throw new ArgumentNullException("Connnection String Null burro");
            }
            else
            {
                _connectionString = _configuration.GetConnectionString("DefaultConnection");
            }           
        }
        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }
}
