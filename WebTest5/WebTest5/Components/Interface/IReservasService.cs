using WebTest5.Components.Model;

namespace WebTest5.Components.Interface
{
    public interface IReservasService
    {        
        Task<IEnumerable<ReservasModel>> GetReservas();
        Task<ReservasModel> GetReserva(int id);
        Task AddReserva(ReservasModel reserva);
        Task UpdateReserva(ReservasModel reserva);
        Task DeleteReserva(int id);
        Task<IEnumerable<GrupoHuespedesModel>> GetGrupoHuespedes(int id);
        Task<IEnumerable<GrupoHabitacionesModel>> GetGrupoHabitaciones(int id);
        Task<IEnumerable<GrupoHabitacionesModel>> GetGrupoHabitaciones();
        Task AddGrupoHuespedes(GrupoHuespedesModel grupoHuespedes);
        Task AddGrupoHabitaciones(GrupoHabitacionesModel grupoHabitaciones);
        Task DeleteGrupoHuespedes(int id);
        Task DeleteGrupoHabitaciones(int id);
        Task<int> GetLastReservaId();
        Task<IEnumerable<HabitacionesModel>> GetAvailableRooms(DateTime fechaLlegada, DateTime fechaSalida);
    }
}
/*
 Task<double> GetHabitacionesCosto(int id);
        Task<double> GetServiciosCosto(int id);
        Task<double> GetCostoFinal(int id);
        Task<IEnumerable<ReservasModel>> SetList(IEnumerable<ReservasModel> reservasList);
 */
