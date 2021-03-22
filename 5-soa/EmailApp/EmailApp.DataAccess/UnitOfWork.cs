using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.DataAccess.EfModel;

namespace EmailApp.DataAccess
{
    // relies on unitofwork, repositories and context all being scoped services
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmailContext _context;

        public IMessageRepository MessageRepository { get; }

        public IAccountRepository AccountRepository { get; }

        public UnitOfWork(EmailContext context, IMessageRepository messageRepository, IAccountRepository accountRepository)
        {
            _context = context;
            MessageRepository = messageRepository;
            AccountRepository = accountRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
