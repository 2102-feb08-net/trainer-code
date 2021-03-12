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
                    Sent = m.Date,
                    Subject = m.Subject
                }).ToListAsync();
        }

        public async Task<Email> GetAsync(int id)
        {
            var message = await _emailContext.Messages
                .Include(m => m.From)
                .FirstAsync(m => m.Id == id);
            return new Email
            {
                Id = message.Id,
                Body = message.Body,
                From = message.From.Address,
                Sent = message.Date,
                Subject = message.Subject
            };
        }

        public void Create(Email email)
        {
            Account account = _emailContext.Accounts.First(a => a.Address == email.From);

            _emailContext.Messages.Add(new Message
            {
                Body = email.Body,
                Subject = email.Subject,
                Date = email.Sent,
                From = account
            });
        }

        public void Delete(int id)
        {
            var message = _emailContext.Messages
                .First(m => m.Id == id);
            _emailContext.Messages.Remove(message);
        }

        public async Task SaveAsync()
        {
            await _emailContext.SaveChangesAsync();
        }
    }
}
