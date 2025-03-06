using Dapper;
using WebTest5.Components.Data;
using WebTest5.Components.Interface;
using WebTest5.Components.Model;


namespace WebTest5.Components.Service
{
    public class CategoriasService : ICategoriasService
    {
        private readonly DataContext _context;
		ILogger _logger;

        public CategoriasService(DataContext context, ILogger<CategoriasService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<PaisesModel>> GetPaises()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<PaisesModel>("SELECT * FROM paises");
        }
        public async Task<IEnumerable<CiudadesModel>> GetCiudades()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<CiudadesModel>("SELECT * FROM ciudades");
        }
        public async Task<IEnumerable<ProfesionesModel>> GetProfesiones()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<ProfesionesModel>("SELECT * FROM profesiones");
        }
        public async Task<IEnumerable<EmpresasModel>> GetEmpresas()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<EmpresasModel>("SELECT * FROM empresas");
        }
        public async Task<IEnumerable<MotivosReservaModel>> GetMotivosReserva()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<MotivosReservaModel>("SELECT * FROM motivos_reserva");
        }
        public async Task<IEnumerable<EstadosReservaModel>> GetEstadosReserva()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<EstadosReservaModel>("SELECT * FROM estados_reserva");
        }
        public async Task<IEnumerable<EstadosHabitacionModel>> GetEstadosHabitacion()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<EstadosHabitacionModel>("SELECT * FROM estados_habitacion");
        }
        public async Task<IEnumerable<CategoriasModel>> GetCategorias( string tabla)
        {
            if (tabla == null)
            {
                throw new ArgumentException("Tabla cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<CategoriasModel>("SELECT * FROM "+ tabla);
        }
        public async Task AddCategoria(CategoriasModel categoria, string tabla)
        {
            if (categoria == null)
            {
                throw new ArgumentException("Categoria cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO " + tabla + " (nombre) VALUES (@Nombre)";
            await connection.ExecuteAsync(sql, new { categoria.Nombre });
        }
        public async Task UpdateCategoria(CategoriasModel categoria, string tabla)
        {
            if (categoria == null)
            {
                throw new ArgumentException("Categoria cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "UPDATE " + tabla + " SET nombre = @Nombre WHERE id = @Id; ";
            await connection.ExecuteAsync(sql, new{ categoria.Nombre,categoria.Id });
        }
        public async Task DeleteCategoria(int Id, string tabla)
        {
            if (Id == null)
            {
                throw new ArgumentException("Categoria cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "DELETE FROM " + tabla + " WHERE id = @Id; ";
            await connection.ExecuteAsync(sql, new { Id });
        }
        public async Task AddProfesion(ProfesionesModel profesion)
        {
            if (profesion == null)
            {
                throw new ArgumentException("Profesion cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO profesiones (nombre) VALUES (@Nombre)";
            await connection.ExecuteAsync(sql, new { profesion.Nombre });
        }
        public async Task AddEmpresa(EmpresasModel empresa)
        {
            if (empresa == null)
            {
                throw new ArgumentException("Empresa cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO empresas (nombre) VALUES (@Nombre)";
            await connection.ExecuteAsync(sql, new { empresa.Nombre });
        }
        public async Task AddCiudad(CiudadesModel ciudad)
        {
            if (ciudad == null)
            {
                throw new ArgumentException("Ciudad cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO ciudades (nombre) VALUES (@Nombre)";
            await connection.ExecuteAsync(sql, new { ciudad.Nombre });
        }
        public async Task AddPais(PaisesModel pais)
        {
            if (pais == null)
            {
                throw new ArgumentException("Pais cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO paises (nombre) VALUES (@Nombre)";
            await connection.ExecuteAsync(sql, new { pais.Nombre });
        }
        public async Task AddMotivoReserva(MotivosReservaModel motivoReserva)
        {
            if (motivoReserva == null)
            {
                throw new ArgumentException("Motivo de Reserva cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO motivos_reserva (nombre) VALUES (@Nombre)";
            await connection.ExecuteAsync(sql, new { motivoReserva.Nombre });
        }
        public async Task UpdateEmpresa(EmpresasModel empresa)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("UPDATE empresas SET nombre = @Nombre WHERE id = @Id;", new { empresa.Id, empresa.Nombre });
        }
    }
}
