using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.DataAccess.EfModel;
using Microsoft.EntityFrameworkCore;

namespace EmailApp.DataAccess
{
    public class AccountRepository : IAccountRepository
    {
        private readonly EmailContext _emailContext;

        public AccountRepository(EmailContext emailContext)
        {
            _emailContext = emailContext;
        }

        public async Task<bool> AddIfNotExistsAsync(string address)
        {
            if (!await _emailContext.Accounts.AnyAsync(a => a.Address == address))
            {
                _emailContext.Accounts.Add(new Account { Address = address });
                return true;
            }
            return false;
        }
    }
}
