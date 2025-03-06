namespace WebTest5.Components.Interface
{
    public interface ILoginService
    {
        Task<bool> IsValidUser(string username, string password);
        Task<int> GetNivelAcceso(string username);
    }
}
