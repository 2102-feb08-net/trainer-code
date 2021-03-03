using System.Collections.Generic;

namespace EmailApp.Business
{
    public interface IMessageRepository
    {
        void Create(Email email);
        Email Get(int id);
        IEnumerable<Email> List();
        void Save();
    }
}