using Dapper;
using WebTest5.Components.Data;
using WebTest5.Components.Interface;
using WebTest5.Components.Model;

namespace WebTest5.Components.Service
{
    public class HabitacionesService : IHabitacionesService
    {
        private readonly DataContext _context;

        public HabitacionesService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HabitacionesModel>> GetHabitaciones()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<HabitacionesModel>("SELECT * FROM habitaciones");            
        }
        public async Task<HabitacionesModel> GetHabitacion(int? id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<HabitacionesModel>("SELECT * FROM habitaciones WHERE id = @id", new { Id = id });
        }
        public async Task AddHabitacion(HabitacionesModel habitacion)
        {
            if (habitacion == null)
            {
                throw new ArgumentException("Habitacion cannot be null or empty.");
            }
			using var connection = _context.CreateConnection();
            var sql = "INSERT INTO habitaciones (nombre, tipo, id_estado) VALUES (@Nombre, @Tipo, @Id_Estado)";
            await connection.ExecuteAsync(sql, new { habitacion.Nombre, habitacion.Tipo,habitacion.Id_Estado});
        }
        public async Task UpdateHabitacion(HabitacionesModel habitacion)
        {
			using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("UPDATE habitaciones SET nombre = @Nombre, tipo = @Tipo, id_estado = @Id_Estado WHERE id = @Id;", new { habitacion.Id,habitacion.Nombre, habitacion.Tipo, habitacion.Id_Estado});
        }
        public async Task DeleteHabitacion(int id)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM habitaciones WHERE id = @Id", new { Id = id });
        }
        public async Task<IEnumerable<TipoHabitacionesModel>> GetTipoHabitaciones()
		{
			using var connection = _context.CreateConnection();
			return await connection.QueryAsync<TipoHabitacionesModel>("SELECT * FROM tipo_habitaciones");
		}
		public async Task<TipoHabitacionesModel> GetTipoHabitacion(int id)
		{
			using var connection = _context.CreateConnection();
			return await connection.QueryFirstOrDefaultAsync<TipoHabitacionesModel>("SELECT * FROM tipo_habitaciones WHERE id = @id", new { Id = id });
		}
	}
}
