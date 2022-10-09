namespace FirstDemo.Infrastructure.Services
{
    public interface IDataUtility
    {
        Task InsertDataAsync(string command);
    }
}