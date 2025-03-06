using WebTest5.Components.Service;

namespace WebTest5.Components.Interface
{
    public interface IBiService
    {
        Task Iniciar();
        Task AddPrediccion(List<HotelBooking> predicciones);
    }
}
