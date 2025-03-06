using Dapper;
using Microsoft.Extensions.Logging;
using WebTest5.Components.Data;
using WebTest5.Components.Interface;
using WebTest5.Components.Model;

namespace WebTest5.Components.Service
{
    public class ClientesService : IClientesService
    {
        private readonly DataContext _context;
        //ILogger _logger;

		public ClientesService(DataContext context)//, ILogger<ClientesService> logger)
        {
			_context = context;
            //_logger = logger;
        }        
        public async Task<IEnumerable<ClientesModel>> GetClientes()
        {
            using var connection = _context.CreateConnection();
            //_logger.LogError("Mensaje");-
            return await connection.QueryAsync<ClientesModel>("SELECT * FROM clientes");            
        }
        public async Task<ClientesModel> GetCliente(int? id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ClientesModel>("SELECT * FROM clientes WHERE cid = @Cid", new { Cid = id });
        }
        public async Task AddCliente(ClientesModel cliente)
        {
            if (cliente == null || string.IsNullOrEmpty(cliente.Nombre_Completo))
            {
                throw new ArgumentException("Cliente name cannot be null or empty.");
            }
            string fecha_Nacimiento = (cliente.Fecha_Nacimiento.Year + "-" + cliente.Fecha_Nacimiento.Month + "-" + cliente.Fecha_Nacimiento.Day);
			using var connection = _context.CreateConnection();
			int? ciudad;
			if (cliente.Ciudad_Procedencia > 0)
				ciudad = cliente.Ciudad_Procedencia;
			else
				ciudad = null;
			int? profesion;
			if (cliente.Profesion > 0)
				profesion = cliente.Profesion;
			else
				profesion = null;
			var sql = "INSERT INTO clientes (cid, nombre_completo, direccion_residencia, correo_electronico, pais, ciudad_procedencia, profesion, fecha_nacimiento, telefono)" +
							" VALUES (@Cid, @Nombre_Completo, @Direccion_Residencia, @Correo_Electronico, @Pais, @ciudad, @profesion, @Fecha_Nacimiento, @Telefono)";
            await connection.ExecuteAsync(sql,
                new { cliente.Cid, cliente.Nombre_Completo, cliente.Direccion_Residencia, cliente.Correo_Electronico, cliente.Pais, ciudad, profesion, fecha_Nacimiento, cliente.Telefono });
        }
        public async Task UpdateCliente(ClientesModel cliente, int? Id)
        {
			string fecha_Nacimiento = (cliente.Fecha_Nacimiento.Year + "-" + cliente.Fecha_Nacimiento.Month + "-" + cliente.Fecha_Nacimiento.Day);

			using var connection = _context.CreateConnection();
			int? ciudad;
			if (cliente.Ciudad_Procedencia > 0)
				ciudad = cliente.Ciudad_Procedencia;
			else
				ciudad = null;
			int? profesion;
			if (cliente.Profesion > 0)
				profesion = cliente.Profesion;
			else
				profesion = null;
			await connection.ExecuteAsync("UPDATE clientes SET cid=@Cid, nombre_completo = @Nombre_Completo, direccion_residencia = @Direccion_Residencia, correo_electronico = @Correo_Electronico, pais = @Pais, ciudad = @ciudad, profesion = @profesion, fecha_nacimiento = @Fecha_Nacimiento, telefono = @Telefono WHERE cid = @Id;", 
                new {Id, cliente.Cid, cliente.Nombre_Completo, cliente.Direccion_Residencia, cliente.Correo_Electronico, cliente.Pais, ciudad, profesion, fecha_Nacimiento, cliente.Telefono });
        }
        public async Task DeleteCliente(int id)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM clientes WHERE cid = @Cid", new { Cid = id });
        }
    }
}
