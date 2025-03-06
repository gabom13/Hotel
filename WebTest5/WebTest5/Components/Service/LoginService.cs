using MySql.Data.MySqlClient;
using Dapper;
using WebTest5.Components.Model;
using WebTest5.Components.Interface;
using WebTest5.Components.Data;

namespace WebTest5.Components.Service
{
    public class LoginService:ILoginService
    {
        private readonly string _connectionString;
        private readonly DataContext _context;

        public LoginService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> IsValidUser(string username, string password)
        {
            using var connection = _context.CreateConnection();
            var user = await connection.QueryAsync<UsuariosModel>("SELECT * FROM usuarios WHERE cid = @username AND contraseña = @password",
                new { username, password });
            if(user != null)
            {
                return user.Count() > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> GetNivelAcceso(string username)
        {
            using var connection = _context.CreateConnection();
            var user = await connection.QueryFirstAsync<int>("SELECT nivel_acceso FROM usuarios WHERE cid = @username",
                new { username});
            return user;
        }
    }
}

