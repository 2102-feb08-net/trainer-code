using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailApp.DataAccess;
using EmailApp.DataAccess.EfModel;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmailApp.IntegrationTests
{
    public class MessageRepositoryTests
    {
        [Fact]
        public async Task Get_GetsExistingEmail()
        {
            // arrange
            using var contextFactory = new TestEmailContextFactory();
            using EmailContext context = contextFactory.CreateContext();
            // insert test data here, or maybe have a helper method somewhere to possibly share it between tests
            var insertedEmail = new Message
            {
                Body = "asdf",
                Date = new DateTimeOffset(2021, 1, 1, 0, 0, 0, new TimeSpan(0)),
                From = new Account { Address = "from@from.com" },
                Subject = "example subject"
            };
            context.Messages.Add(insertedEmail);
            context.SaveChanges();
            var repo = new MessageRepository(context);

            // act
            Business.Email email = await repo.GetAsync(insertedEmail.Id);

            // assert
            Assert.Equal(insertedEmail.Id, email.Id);
            Assert.Equal(insertedEmail.Body, email.Body);
            Assert.Equal(insertedEmail.Date, email.Sent);
            Assert.Equal(insertedEmail.From.Address, email.From);
            Assert.Equal(insertedEmail.Subject, email.Subject);
        }
        // should also test error scenarios

        [Fact]
        public void Create_CreateValidEmail()
        {
            // arrange
            using var contextFactory = new TestEmailContextFactory();
            using var context = contextFactory.CreateContext();
            var from = new Account { Address = "from@from.com" };
            context.Accounts.Add(from);
            context.SaveChanges();
            var emailToCreate = new Business.Email
            {
                Body = "asdf",
                Sent = new DateTimeOffset(2021, 1, 1, 0, 0, 0, new TimeSpan(0)),
                From = from.Address,
                Subject = "example subject"
            };
            var repo = new MessageRepository(context);

            // act
            repo.Create(emailToCreate);
            // (that method doesn't savechanges, so i need to use the same context instance to verify)

            // assert
            Message email = context.Messages.Local.Single(m => m.Date == emailToCreate.Sent);
            Assert.Equal(emailToCreate.Id, email.Id);
            Assert.Equal(emailToCreate.Body, email.Body);
            Assert.Equal(emailToCreate.Sent, email.Date);
            Assert.Equal(emailToCreate.From, email.From.Address);
            Assert.Equal(emailToCreate.Subject, email.Subject);
        }
        // should also test error scenarios

        // should also test "saving no changes"
        [Fact]
        public void Save_SavesOneCreate()
        {
            // arrange
            using var contextFactory = new TestEmailContextFactory();
            var from = new Account { Address = "from@from.com" };
            var emailToCreate = new Business.Email
            {
                Body = "asdf",
                Sent = new DateTimeOffset(2021, 1, 1, 0, 0, 0, new TimeSpan(0)),
                From = from.Address,
                Subject = "example subject"
            };
            using (var arrangeActContext = contextFactory.CreateContext())
            {
                arrangeActContext.Accounts.Add(from);
                arrangeActContext.SaveChanges();
                var repo = new MessageRepository(arrangeActContext);
                repo.Create(emailToCreate);
                // a email is ready to be saved, but not saved yet

                // act
                repo.SaveAsync();
            }

            // assert
            using var assertContext = contextFactory.CreateContext();
            Message email = assertContext.Messages.Include(m => m.From).Single(m => m.Date == emailToCreate.Sent);
            Assert.Equal(emailToCreate.Body, email.Body);
            Assert.Equal(emailToCreate.Sent, email.Date);
            Assert.Equal(emailToCreate.From, email.From.Address);
            Assert.Equal(emailToCreate.Subject, email.Subject);
        }
        // should also test error scenarios

        // rule of thumb - have separate context instances for all three stages, arrange, act, and assert
        // to ensure everything that should go all the way to/from the database does and nothing else
    }
}
