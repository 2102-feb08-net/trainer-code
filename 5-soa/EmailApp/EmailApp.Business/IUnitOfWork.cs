using System.Threading.Tasks;

namespace EmailApp.Business
{
    public interface IUnitOfWork
    {
        IMessageRepository MessageRepository { get; }

        IAccountRepository AccountRepository { get; }

        Task SaveAsync();
    }
}