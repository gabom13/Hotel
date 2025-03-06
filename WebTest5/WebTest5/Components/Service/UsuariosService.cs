using Dapper;
using Microsoft.Extensions.Logging;
using WebTest5.Components.Data;
using WebTest5.Components.Interface;
using WebTest5.Components.Model;

namespace WebTest5.Components.Service
{
    public class UsuariosService : IUsuariosService
    {
        private readonly DataContext _context;
        ILogger _logger;

		public UsuariosService(DataContext context, ILogger<ClientesService> logger)
        {
			_context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<UsuariosModel>> GetUsuarios()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<UsuariosModel>("SELECT * FROM usuarios");            
        }
        public async Task<IEnumerable<NivelesAccesoModel>> GetNivelesAcceso()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<NivelesAccesoModel>("SELECT * FROM niveles_acceso");
        }
        public async Task<UsuariosModel> GetUsuario(int? id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<UsuariosModel>("SELECT * FROM usuarios WHERE cid = @Cid", new { Cid = id });
        }
        public async Task AddUsuario(UsuariosModel usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Nombre_Completo))
            {
                throw new ArgumentException("Usuario name cannot be null or empty.");
            }
            string fecha_Nacimiento = (usuario.Fecha_Nacimiento.Year + "-" + usuario.Fecha_Nacimiento.Month + "-" + usuario.Fecha_Nacimiento.Day);
			using var connection = _context.CreateConnection();
            var sql = "INSERT INTO usuarios (cid, nombre_completo, fecha_nacimiento, telefono, contraseña, nivel_acceso)" +
                            " VALUES (@Cid, @Nombre_Completo, @Fecha_Nacimiento, @Telefono, @Contraseña, @Nivel_Acceso)";
            await connection.ExecuteAsync(sql,
                new {usuario.Cid, usuario.Nombre_Completo, fecha_Nacimiento, usuario.Telefono, usuario.Contraseña, usuario.Nivel_Acceso });
        }
        public async Task UpdateUsuario(UsuariosModel usuario, int? Id)
        {
			string fecha_Nacimiento = (usuario.Fecha_Nacimiento.Year + "-" + usuario.Fecha_Nacimiento.Month + "-" + usuario.Fecha_Nacimiento.Day);

			using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("UPDATE usuarios SET cid = @Cid, nombre_completo = @Nombre_Completo, fecha_nacimiento = @Fecha_Nacimiento, telefono = @Telefono, contraseña = @Contraseña, nivel_acceso = @Nivel_Acceso WHERE cid = @Id;",
                 new {Id, usuario.Cid, usuario.Nombre_Completo, fecha_Nacimiento, usuario.Telefono, usuario.Contraseña, usuario.Nivel_Acceso });
        }
        public async Task DeleteUsuario(int id)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM usuarios WHERE cid = @Cid", new { Cid = id });
        }
    }
}
