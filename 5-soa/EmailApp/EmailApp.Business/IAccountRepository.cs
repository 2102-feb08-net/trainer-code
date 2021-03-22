using System.Threading.Tasks;

namespace EmailApp.Business
{
    public interface IAccountRepository
    {
        Task<bool> AddIfNotExistsAsync(string address);
    }
}
