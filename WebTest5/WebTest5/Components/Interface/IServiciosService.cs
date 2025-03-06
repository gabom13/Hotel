using WebTest5.Components.Model;

namespace WebTest5.Components.Interface
{
    public interface IServiciosService
    {        
        Task<IEnumerable<BoletasServicioModel>> GetBoletasServicio();
        Task<IEnumerable<ServiciosModel>> GetServicios();
        Task<BoletasServicioModel> GetBoletaServicio(int id);
        Task<ServiciosModel> GetServicio(int id);
        Task AddBoletaServicio(BoletasServicioModel boletaServicio);
        Task AddServicio(ServiciosModel servicio);
        Task UpdateBoletaServicio(BoletasServicioModel boletaServicio);
        Task UpdateServicio(ServiciosModel servicio);
        Task DeleteBoletaServicio(int id);
        Task DeleteServicio(int id);
    }
}
