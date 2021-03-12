using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailApp.Business
{
    public class InboxCleaner : IInboxCleaner
    {
        private readonly IMessageRepository _messageRepository;

        public InboxCleaner(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task CleanInboxAsync()
        {
            var messages = await _messageRepository.ListAsync();
            IEnumerable<Email> spam = messages
                .Where(e => e.IsSpam());

            foreach (int id in spam.Select(e => e.Id))
            {
                _messageRepository.Delete(id);
            }
            await _messageRepository.SaveAsync();
        }
    }
}
