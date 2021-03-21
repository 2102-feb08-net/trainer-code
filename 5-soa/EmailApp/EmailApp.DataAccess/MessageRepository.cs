using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.Business;
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

        public async Task<IEnumerable<Email>> ListAsync()
        {
            return await _emailContext.Messages
                .Include(m => m.From)
                .Where(m => !m.IsDeleted)
                .Select(m => new Email
                {
                    Id = m.Guid,
                    Body = m.Body,
                    From = m.From.Address,
                    OrigDate = m.OrigDate,
                    Subject = m.Subject
                }).ToListAsync();
        }

        public async Task<Email> GetAsync(Guid id)
        {
            if (await _emailContext.Messages
                .Include(m => m.From)
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

        public async Task<Email> CreateAsync(Email email)
        {
            if (await _emailContext.Accounts
                .FirstOrDefaultAsync(a => a.Address == email.From) is not Account from)
            {
                throw new ArgumentException($"account {email.From} not found", nameof(email));
            }
            Account to = null;
            if (email.To is not null)
            {
                to = await _emailContext.Accounts
                    .FirstOrDefaultAsync(a => a.Address == email.To);
                if (to is null)
                {
                    throw new ArgumentException($"account {email.To} not found", nameof(email));
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

        public async Task DeleteAsync(Guid id)
        {
            if (await _emailContext.Messages
                .FirstOrDefaultAsync(m => m.Guid == id) is not Message message)
            {
                throw new ArgumentException("message not found", nameof(id));
            }
            _emailContext.Messages.Remove(message);
        }
    }
}
