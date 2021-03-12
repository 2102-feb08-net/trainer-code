using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.DataAccess.EfModel;

namespace EmailApp.DataAccess
{
    public class UnitOfWork
    {
        private readonly EmailContext _context;

        public IMessageRepository MessageRepository { get; }
        public IMessageRepository MessageRepository2 { get; }

        public UnitOfWork(EmailContext context)
        {
            MessageRepository = new MessageRepository(context);
            MessageRepository2 = new MessageRepository(context);
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // unitOfWork.StoreRepository.Update(store);
        // unitOfWork.OrderRepository.Create(order);
        // unitOfWork.Save();
    }
}
