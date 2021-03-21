using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailApp.Business
{
    public interface IMessageRepository
    {
        Task<Email> GetByIdAsync(Guid id);
        Task<IEnumerable<Email>> ListAsync();
        Task<IEnumerable<Email>> ListByRecipientAsync(string to);
        Task<Email> AddAsync(Email email);
        Task DeleteByIdAsync(Guid id);
    }
}