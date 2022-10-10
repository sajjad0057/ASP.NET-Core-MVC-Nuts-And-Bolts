namespace FirstDemo.Infrastructure.Services
{
    public interface IDataUtility
    {
        Task ExecuteCommandAsync(string command, Dictionary<string, object> parameters);

        Task<List<Dictionary<string, object>>> GetDataAsync(string command, Dictionary<string, object> parameters);
    }
}