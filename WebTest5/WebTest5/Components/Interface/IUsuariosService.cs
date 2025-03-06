using WebTest5.Components.Model;

namespace WebTest5.Components.Interface
{
    public interface IUsuariosService
    {        
        Task<IEnumerable<UsuariosModel>> GetUsuarios();
        Task<IEnumerable<NivelesAccesoModel>> GetNivelesAcceso();
        Task<UsuariosModel> GetUsuario(int? id);
        Task AddUsuario(UsuariosModel usuario);
        Task UpdateUsuario(UsuariosModel usuario, int? id);
        Task DeleteUsuario(int id);
    }
}
