using System;
using System.Linq;
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
                Guid = Guid.NewGuid(),
                OrigDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, new TimeSpan(0)),
                From = new Account { Address = "from@from.com" },
                Subject = "example subject",
                Body = "asdf"
            };
            context.Messages.Add(insertedEmail);
            context.SaveChanges();
            var repo = new MessageRepository(context);

            // act
            Business.Email email = await repo.GetByIdAsync(insertedEmail.Guid);

            // assert
            Assert.Equal(insertedEmail.Guid, email.Id);
            Assert.Equal(insertedEmail.Body, email.Body);
            Assert.Equal(insertedEmail.OrigDate, email.OrigDate);
            Assert.Equal(insertedEmail.From.Address, email.From);
            Assert.Equal(insertedEmail.Subject, email.Subject);
        }
        // should also test error scenarios

        [Fact]
        public async Task Create_CreateValidEmail()
        {
            // arrange
            var from = new Account { Address = "from@from.com" };
            var emailToCreate = new Business.Email
            {
                Id = Guid.NewGuid(),
                OrigDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, new TimeSpan(0)),
                From = from.Address,
                Subject = "example subject",
                Body = "asdf"
            };
            using var contextFactory = new TestEmailContextFactory();
            using var context = contextFactory.CreateContext();
            context.Accounts.Add(from);
            context.SaveChanges();
            var repo = new MessageRepository(context);

            // act
            var returnedEmail = await repo.AddAsync(emailToCreate);
            // (that method doesn't save changes, so i need to use the same context instance to verify)

            // assert
            Message email = context.Messages.Local.Single(m => m.Guid == emailToCreate.Id);
            Assert.Equal(EntityState.Added, context.Entry(email).State);
            Assert.Equal(emailToCreate.Id, email.Guid);
            Assert.Equal(emailToCreate.OrigDate, email.OrigDate);
            Assert.Equal(emailToCreate.From, email.From.Address);
            Assert.Equal(emailToCreate.Subject, email.Subject);
            Assert.Equal(emailToCreate.Body, email.Body);
        }
        // should also test error scenarios

        // when testing code that uses the context,
        // consider having separate context instances for all three stages, arrange, act, and assert
        // to ensure everything that should go all the way to/from the database does and nothing else
    }
}
