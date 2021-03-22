using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.Business.Exceptions;
using EmailApp.DataAccess.EfModel;
using Microsoft.EntityFrameworkCore;

namespace EmailApp.DataAccess
{
    public class MessageRepository : IMessageRepository
    {
        private readonly EmailContext _emailContext;

        public MessageRepository(EmailContext emailContext)
        {
            _emailContext = emailContext;
        }

        public async Task<Email> GetByIdAsync(Guid id)
        {
            if (await _emailContext.Messages
                .Include(m => m.From)
                .Include(m => m.To)
                .Where(m => !m.IsDeleted)
                .FirstOrDefaultAsync(m => m.Guid == id) is not Message message)
            {
                return null;
            }
            return new Email
            {
                Id = message.Guid,
                OrigDate = message.OrigDate,
                From = message.From.Address,
                To = message.To?.Address,
                Body = message.Body,
                Subject = message.Subject
            };
        }

        public async Task<IEnumerable<Email>> ListAsync()
        {
            return await _emailContext.Messages
                .Include(m => m.From)
                .Include(m => m.To)
                .Where(m => !m.IsDeleted)
                .Select(m => new Email
                {
                    Id = m.Guid,
                    OrigDate = m.OrigDate,
                    From = m.From.Address,
                    To = m.To != null ? m.To.Address : null,
                    Subject = m.Subject,
                    Body = m.Body
                }).ToListAsync();
        }

        public async Task<IEnumerable<Email>> ListByRecipientAsync(string address)
        {
            if (await _emailContext.Accounts
                .Include(a => a.ReceivedMessages.Where(m => !m.IsDeleted))
                    .ThenInclude(m => m.From)
                .FirstOrDefaultAsync(a => a.Address == address) is not Account account)
            {
                return null;
            }
            return account.ReceivedMessages.Select(m => new Email
            {
                Id = m.Guid,
                OrigDate = m.OrigDate,
                From = m.From.Address,
                To = m.To.Address,
                Subject = m.Subject,
                Body = m.Body
            });
        }

        public async Task<Email> AddAsync(Email email)
        {
            if (await _emailContext.Accounts
                .FirstOrDefaultAsync(a => a.Address == email.From) is not Account from)
            {
                throw new OriginatorAddressNotFoundException();
            }
            Account to = null;
            if (email.To is not null)
            {
                to = await _emailContext.Accounts
                    .FirstOrDefaultAsync(a => a.Address == email.To);
                if (to is null)
                {
                    throw new DestinationAddressNotFoundException();
                }
            }

            if (email.Id == default)
            {
                email.Id = Guid.NewGuid();
            }
            var entity = new Message
            {
                Guid = email.Id,
                OrigDate = email.OrigDate,
                From = from,
                To = to,
                Subject = email.Subject,
                Body = email.Body
            };
            _emailContext.Messages.Add(entity);

            return email;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            if (await _emailContext.Messages
                .FirstOrDefaultAsync(m => m.Guid == id) is not Message message)
            {
                throw new ArgumentException("message not found", nameof(id));
            }
            message.IsDeleted = true;
        }
    }
}
