using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailApp.Business
{
    public interface IMessageRepository
    {
        void Create(Email email);
        Task<Email> GetAsync(int id);
        Task<IEnumerable<Email>> ListAsync();
        void Delete(int id);
        Task SaveAsync();
    }
}