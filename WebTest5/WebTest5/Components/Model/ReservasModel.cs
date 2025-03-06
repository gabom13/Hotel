namespace WebTest5.Components.Model
{
	public class ReservasModel
	{
        public int Id { get; set; }
        public int Id_Titular { get; set; }
        public int Id_Estado { get; set; }
        public DateTime Fecha_Llegada { get; set; }
        public DateTime Fecha_Salida { get; set; }
        public int Pax { get; set; }
        public int Descuento { get; set; }
        public DateTime Fecha_Reserva { get; set; }
		public int Motivo_Reserva { get; set; }
		public int Empresa { get; set; }
		public string Nota { get; set; }
        public double PrecioFinal { get; set; }//precio sumado el precio_habitacion y el total a pagar por servicios adicionales
        public double PrecioHabitaciones { get; set; }//precio total de las habitaciones reservadas
        public double PrecioServicios { get; set; }//precio total de los servicios solicitados
        public string NombreMotivoReserva { get; set; }//columna personalizada
        public string NombreEstado { get; set; }//columna personalizada
        public string NombreTitular { get; set; }//columna personalizada
        public string NombreEmpresa { get; set; }//columna personalizada
	}
}
