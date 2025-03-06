using WebTest5.Components.Model;

namespace WebTest5.Components.Interface
{
    public interface IHabitacionesService
    {        
        Task<IEnumerable<HabitacionesModel>> GetHabitaciones();
        Task<HabitacionesModel> GetHabitacion(int? id);
        Task AddHabitacion(HabitacionesModel Habitacion);
        Task UpdateHabitacion(HabitacionesModel Habitacion);
        Task DeleteHabitacion(int id);
		Task<IEnumerable<TipoHabitacionesModel>> GetTipoHabitaciones();
		Task<TipoHabitacionesModel> GetTipoHabitacion(int id);

	}
}
