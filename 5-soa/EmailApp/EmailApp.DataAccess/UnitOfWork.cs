using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.DataAccess.EfModel;

namespace EmailApp.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmailContext _context;

        public IMessageRepository MessageRepository { get; }

        public UnitOfWork(EmailContext context)
        {
            MessageRepository = new MessageRepository(context);
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
