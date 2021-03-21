using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApp.Business
{
    public class InboxCleaner : IInboxCleaner
    {
        private readonly IUnitOfWork _unitOfWork;

        public InboxCleaner(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CleanInboxAsync(string address)
        {
            var messages = await _unitOfWork.MessageRepository.ListByRecipientAsync(address);
            IEnumerable<Email> spam = messages
                .Where(e => e.IsSpam());

            foreach (Guid id in spam.Select(e => e.Id))
            {
                await _unitOfWork.MessageRepository.DeleteByIdAsync(id);
            }
            await _unitOfWork.SaveAsync();
        }
    }
}
