using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailApp.Business
{
    public interface IMessageRepository
    {
        Task<Email> GetAsync(Guid id);
        Task<IEnumerable<Email>> ListAsync();
        Task<Email> CreateAsync(Email email);
        Task DeleteAsync(Guid id);
    }
}