using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailApp.Business
{
    public interface IMessageRepository
    {
        Task<Email> CreateAsync(Email email);
        Task<Email> GetAsync(int id);
        Task<IEnumerable<Email>> ListAsync();
        Task DeleteAsync(int id);
    }
}