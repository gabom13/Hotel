using Dapper;
using WebTest5.Components.Data;
using WebTest5.Components.Interface;
using WebTest5.Components.Model;
using WebTest5.Components.Pages;

namespace WebTest5.Components.Service
{
    public class ServicioService : IServiciosService
    {
        private readonly DataContext _context;
        ILogger _logger;

		public ServicioService(DataContext context, ILogger<HabitacionesService> logger)
        {
			_context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<BoletasServicioModel>> GetBoletasServicio()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<BoletasServicioModel>("SELECT * FROM boletas_servicio");
        }
        public async Task<IEnumerable<ServiciosModel>> GetServicios()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<ServiciosModel>("SELECT * FROM servicios");
        }
        public async Task<BoletasServicioModel> GetBoletaServicio(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<BoletasServicioModel>("SELECT * FROM boletas_servicio WHERE id = @id", new { Id = id });
        }
        public async Task<ServiciosModel> GetServicio(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ServiciosModel>("SELECT * FROM servicios WHERE id = @id", new { Id = id });
        }
        public async Task AddBoletaServicio(BoletasServicioModel boletaServicio)
        {
            if (boletaServicio == null)
            {
                throw new ArgumentException("Boleta de Servicio cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO boletas_servicio (id_reserva, servicio, precio, usuario, nota) VALUES (@Id_Reserva, @Servicio, @Precio, @Usuario, @Nota)";
            await connection.ExecuteAsync(sql, new { boletaServicio.Id_Reserva, boletaServicio.Servicio, boletaServicio.Precio, boletaServicio.Usuario, boletaServicio.Nota });
        }
        public async Task AddServicio(ServiciosModel servicio)
        {
            if (servicio == null)
            {
                throw new ArgumentException("Servicio cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO servicios (nombre) VALUES (@Nombre)";
            await connection.ExecuteAsync(sql, new { servicio.Nombre});
        }
        public async Task UpdateBoletaServicio(BoletasServicioModel boletaServicio)
        {
            using var connection = _context.CreateConnection();
            var sql = "UPDATE boletas_servicio SET id_reserva = @Id_Reserva, servicio = @Servicio, precio = @Precio, usuario = @Usuario , nota = @Nota WHERE id = @Id;";
            await connection.ExecuteAsync(sql, new { boletaServicio.Id, boletaServicio.Id_Reserva, boletaServicio.Servicio, boletaServicio.Precio, boletaServicio.Usuario, boletaServicio.Nota});
        }
        public async Task UpdateServicio(ServiciosModel servicio)
        {
            using var connection = _context.CreateConnection();
            var sql = "UPDATE servicios SET nombre = @Nombre WHERE id = @Id;";
            await connection.ExecuteAsync(sql, new {servicio.Id, servicio.Nombre});
        }
        public async Task DeleteBoletaServicio(int id)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM boletas_servicio WHERE id = @Id", new { Id = id });
        }
        public async Task DeleteServicio(int id)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM servicios WHERE id = @Id", new { Id = id });
        }
    }
}
