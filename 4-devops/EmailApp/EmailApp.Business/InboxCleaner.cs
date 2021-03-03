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

        public void CleanInbox()
        {
            IEnumerable<Email> spam = _messageRepository.List()
                .Where(e => e.IsSpam());

            foreach (int id in spam.Select(e => e.Id))
            {
                _messageRepository.Delete(id);
            }
            _messageRepository.Save();
        }
    }
}
