using WebTest5.Components.Model;

namespace WebTest5.Components.Interface
{
    public interface ICategoriasService
    {        
        Task<IEnumerable<PaisesModel>> GetPaises();
        Task<IEnumerable<CiudadesModel>> GetCiudades();
        Task<IEnumerable<ProfesionesModel>> GetProfesiones();
        Task<IEnumerable<EmpresasModel>> GetEmpresas();
        Task<IEnumerable<MotivosReservaModel>> GetMotivosReserva();
        Task<IEnumerable<EstadosReservaModel>> GetEstadosReserva();
        Task<IEnumerable<EstadosHabitacionModel>> GetEstadosHabitacion();
        Task<IEnumerable<CategoriasModel>> GetCategorias(string tabla);
        Task AddCategoria(CategoriasModel categoria, string tabla);
        Task UpdateCategoria(CategoriasModel categoria, string tabla);
        Task DeleteCategoria(int id, string tabla);
        Task AddProfesion(ProfesionesModel profesion);
        Task AddEmpresa(EmpresasModel empresa);
        Task AddCiudad(CiudadesModel ciudad);
        Task AddPais(PaisesModel pais);
        Task AddMotivoReserva(MotivosReservaModel motivoReserva);
        Task UpdateEmpresa(EmpresasModel empresa);
    }
}
