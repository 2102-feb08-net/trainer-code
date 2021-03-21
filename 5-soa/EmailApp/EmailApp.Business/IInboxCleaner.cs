using System.Threading.Tasks;

namespace EmailApp.Business
{
    public interface IInboxCleaner
    {
        Task CleanInboxAsync(string address);
    }
}