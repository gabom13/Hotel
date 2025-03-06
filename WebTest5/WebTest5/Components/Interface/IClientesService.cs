using WebTest5.Components.Model;

namespace WebTest5.Components.Interface
{
    public interface IClientesService
    {        
        Task<IEnumerable<ClientesModel>> GetClientes();
        Task<ClientesModel> GetCliente(int? id);
        Task AddCliente(ClientesModel cliente);
        Task UpdateCliente(ClientesModel cliente,int? Id);
        Task DeleteCliente(int id);
    }
}
