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
                .Select(m => new Email
                {
                    Id = m.Id,
                    Body = m.Body,
                    From = m.From.Address,
                    OrigDate = m.Date,
                    Subject = m.Subject
                }).ToListAsync();
        }

        public async Task<Email> GetAsync(int id)
        {
            if (await _emailContext.Messages
                .Include(m => m.From)
                .FirstOrDefaultAsync(m => m.Id == id) is not Message message)
            {
                return null;
            }
            return new Email
            {
                Id = message.Id,
                Body = message.Body,
                From = message.From.Address,
                OrigDate = message.Date,
                Subject = message.Subject
            };
        }

        public async Task<Email> CreateAsync(Email email)
        {
            Account account = await _emailContext.Accounts.FirstAsync(a => a.Address == email.From);

            var entity = new Message
            {
                Body = email.Body,
                Subject = email.Subject,
                Date = email.OrigDate,
                From = account
            };
            _emailContext.Messages.Add(entity);

            await _emailContext.SaveChangesAsync();

            email.Id = entity.Id;
            return email;
        }

        public async Task DeleteAsync(int id)
        {
            var message = _emailContext.Messages
                .First(m => m.Id == id);
            _emailContext.Messages.Remove(message);

            await _emailContext.SaveChangesAsync();
        }
    }
}
