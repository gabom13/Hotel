using Dapper;
using WebTest5.Components.Data;
using WebTest5.Components.Interface;
using WebTest5.Components.Model;


namespace WebTest5.Components.Service
{
    public class ReservasService : IReservasService
    {
        private readonly DataContext _context;
        ILogger _logger;
        private readonly ICategoriasService categoriasService;
        private readonly IClientesService clientesService;

        public ReservasService(DataContext context, ILogger<ReservasService> logger, ICategoriasService CategoriasService, IClientesService ClientesService)
        {
            _context = context;
            _logger = logger;
            categoriasService = CategoriasService;
            clientesService = ClientesService;
        }
        public async Task<IEnumerable<ReservasModel>> GetReservas()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<ReservasModel>("SELECT * FROM reservas");
        }
        public async Task<ReservasModel> GetReserva(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ReservasModel>("SELECT * FROM reservas WHERE id = @Id", new { Id = id });
        }
        public async Task AddReserva(ReservasModel reserva)
        {
            if (reserva == null)
            {
                throw new ArgumentException("Reservas name cannot be null or empty.");
            }
            string fecha_Llegada = (reserva.Fecha_Llegada.Year + "-" + reserva.Fecha_Llegada.Month + "-" + reserva.Fecha_Llegada.Day);
            string fecha_Salida = (reserva.Fecha_Salida.Year + "-" + reserva.Fecha_Salida.Month + "-" + reserva.Fecha_Salida.Day);
            string fecha_Reserva = (reserva.Fecha_Reserva.Year + "-" + reserva.Fecha_Reserva.Month + "-" + reserva.Fecha_Reserva.Day);
            using var connection = _context.CreateConnection();
            int? empresa;
            if (reserva.Empresa > 0)
                empresa = reserva.Empresa;
            else
                empresa = null;
			int? motivo;
			if (reserva.Motivo_Reserva > 0)
				motivo = reserva.Motivo_Reserva;
			else
				motivo = null;
			int? pax;
			if (reserva.Pax> 0)
				pax = reserva.Pax;
			else
				pax = null;
			int? descuento;
			if (reserva.Descuento> 0)
				descuento = reserva.Descuento;
			else
				descuento = null;
			var sql = "INSERT INTO reservas (id_titular, id_estado, fecha_llegada, fecha_salida, pax, descuento, fecha_reserva, motivo_reserva, empresa, nota)" +
							" VALUES (@Id_Titular, @Id_Estado, @Fecha_Llegada, @Fecha_Salida, @pax, @descuento, @Fecha_Reserva, @motivo, @empresa, @Nota)";
            await connection.ExecuteAsync(sql,
                new { reserva.Id_Titular, reserva.Id_Estado, fecha_Llegada, fecha_Salida, pax, descuento, fecha_Reserva, motivo, empresa    , reserva.Nota });
        }
        public async Task UpdateReserva(ReservasModel reserva)
        {
            string fecha_Llegada = (reserva.Fecha_Llegada.Year + "-" + reserva.Fecha_Llegada.Month + "-" + reserva.Fecha_Llegada.Day);
            string fecha_Salida = (reserva.Fecha_Salida.Year + "-" + reserva.Fecha_Salida.Month + "-" + reserva.Fecha_Salida.Day);
            string fecha_Reserva = (reserva.Fecha_Reserva.Year + "-" + reserva.Fecha_Reserva.Month + "-" + reserva.Fecha_Reserva.Day);
            using var connection = _context.CreateConnection();

            int? empresa;
            if (reserva.Empresa > 0)
                empresa = reserva.Empresa;
            else
                empresa = null;
			int? motivo;
			if (reserva.Motivo_Reserva > 0)
				motivo = reserva.Motivo_Reserva;
			else
				motivo = null;
			int? pax;
			if (reserva.Pax > 0)
				pax = reserva.Pax;
			else
				pax = null;
			int? descuento;
			if (reserva.Descuento > 0)
				descuento = reserva.Descuento;
			else
				descuento = null;
			await connection.ExecuteAsync("UPDATE reservas SET id_titular = @Id_Titular, id_estado = @Id_Estado, fecha_llegada = @Fecha_Llegada, fecha_salida = @Fecha_Salida, pax = @pax, descuento = @descuento, fecha_reserva = @Fecha_Reserva, motivo_reserva = @motivo, empresa = @empresa, nota = @Nota WHERE id = @Id;",
             new { reserva.Id, reserva.Id_Titular, reserva.Id_Estado, fecha_Llegada, fecha_Salida, pax, descuento, fecha_Reserva, motivo, empresa, reserva.Nota });
        }
        public async Task DeleteReserva(int id)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM reservas WHERE id = @Id", new { Id = id });
        }
        public async Task<IEnumerable<GrupoHuespedesModel>> GetGrupoHuespedes(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<GrupoHuespedesModel>("SELECT * FROM grupo_huespedes WHERE id = @Id", new { Id = id });
        }
        public async Task<IEnumerable<GrupoHabitacionesModel>> GetGrupoHabitaciones(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<GrupoHabitacionesModel>("SELECT * FROM grupo_habitaciones WHERE id = @Id", new { Id = id });
        }
        public async Task<IEnumerable<GrupoHabitacionesModel>> GetGrupoHabitaciones()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<GrupoHabitacionesModel>("SELECT * FROM grupo_habitaciones");
        }
        public async Task AddGrupoHuespedes(GrupoHuespedesModel grupoHuespedes)
        {
            if (grupoHuespedes == null)
            {
                throw new ArgumentException("Grupo de huespedes name cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO grupo_huespedes (id, huesped) VALUES (@Id, @Huesped)";
            await connection.ExecuteAsync(sql, new { grupoHuespedes.Id, grupoHuespedes.Huesped });
        }
        public async Task AddGrupoHabitaciones(GrupoHabitacionesModel grupoHabitaciones)
        {
            if (grupoHabitaciones == null)
            {
                throw new ArgumentException("Grupo de habitaciones name cannot be null or empty.");
            }
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO grupo_habitaciones (id, habitacion) VALUES (@Id, @Habitacion)";
            await connection.ExecuteAsync(sql, new { grupoHabitaciones.Id, grupoHabitaciones.Habitacion });
        }
        public async Task DeleteGrupoHuespedes(int id)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM grupo_huespedes WHERE id = @Id", new { Id = id });
        }
        public async Task DeleteGrupoHabitaciones(int id)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM grupo_habitaciones WHERE id = @Id", new { Id = id });
        }
        public async Task<int> GetLastReservaId()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<int>("SELECT id FROM reservas ORDER BY id DESC LIMIT 1",
                "SELECT AUTO_INCREMENT AS next_id FROM information_schema.tables WHERE table_name = 'reservas' AND table_schema = 'sql10715093'");
        }
        public async Task<IEnumerable<HabitacionesModel>> GetAvailableRooms(DateTime fechaLlegada, DateTime fechaSalida)
        {
            using var connection = _context.CreateConnection();
            string fecha_Llegada = (fechaLlegada.Year + "-" + fechaLlegada.Month + "-" + fechaLlegada.Day);
            string fecha_Salida = (fechaSalida.Year + "-" + fechaSalida.Month + "-" + fechaSalida.Day);
            var sql = @"SELECT h.* FROM habitaciones h WHERE h.id NOT IN(SELECT gh.habitacion FROM grupo_habitaciones gh
                        INNER JOIN reservas res ON gh.id = res.id
                        WHERE(res.fecha_llegada <= @fecha_Salida AND res.fecha_salida >= @fecha_Llegada) AND (res.id_estado = '1'))
                        ORDER BY h.id ASC;";
            var habitacionesDisponibles = await connection.QueryAsync<HabitacionesModel>(sql, new { fecha_Llegada, fecha_Salida });
            return habitacionesDisponibles;
        }
    }
}

/*
 * 
 *  /* public async Task<double> GetHabitacionesCosto(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = @"SELECT SUM(th.precio) AS total_costo_habitacion FROM grupo_habitaciones gh 
                        JOIN habitaciones h ON gh.habitacion = h.id JOIN tipo_habitaciones th ON h.tipo = th.id
                        WHERE gh.id = @Id";
            double costoHabitaciones = await connection.QueryFirstAsync<int>(sql, new { id });
            double descuento = (await GetReserva(id)).Descuento;
            descuento = costoHabitaciones * (descuento / 100);
            costoHabitaciones = costoHabitaciones - descuento;
            return costoHabitaciones;
        }
        public async Task<double> GetServiciosCosto(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = @"SELECT COALESCE(SUM(bs.precio), 0) AS total_costo_servicios FROM boletas_servicio bs
                        JOIN reservas r ON bs.id_reserva = r.id WHERE r.id = @Id;";
            double? costoServiciosNullable = await connection.QueryFirstOrDefaultAsync<double>(sql, new { id });
            double costoServicios = costoServiciosNullable ?? 0.0;
            return costoServicios;
        }
        public async Task<double> GetCostoFinal(int id)
        {
            using var connection = _context.CreateConnection();
            double costoFinal = await GetHabitacionesCosto(id) + await GetServiciosCosto(id);
            return costoFinal;
        }
        public async Task<IEnumerable<ReservasModel>> SetList(IEnumerable<ReservasModel> reservasList)
        {
            foreach (var r in reservasList)
            {
                r.NombreEstado = (await categoriasService.GetEstadosReserva()).FirstOrDefault(e => e.Id == r.Id_Estado).Nombre;
                r.NombreEmpresa = (await categoriasService.GetEmpresas()).FirstOrDefault(e => e.Id == r.Empresa)?.Nombre ?? "";
                r.NombreMotivoReserva = (await categoriasService.GetMotivosReserva()).FirstOrDefault(m => m.Id == r.Motivo_Reserva)?.Nombre ?? "";
                r.NombreTitular = (await clientesService.GetClientes()).FirstOrDefault(t => t.Cid == r.Id_Titular).Nombre_Completo;
                r.PrecioHabitaciones = await GetHabitacionesCosto(r.Id);
                r.PrecioServicios = await GetServiciosCosto(r.Id);
                r.PrecioFinal = await GetCostoFinal(r.Id);
            }
            return reservasList;
        }
SELECT h.* FROM habitaciones h WHERE h.id NOT IN (
                        SELECT gh.habitacion FROM grupo_habitaciones gh INNER JOIN reservas res ON gh.id = res.id
                        WHERE res.fecha_llegada <= '2024-08-07' AND res.fecha_salida >='2024-08-14'
                        )
                        ORDER BY h.id ASC
 */