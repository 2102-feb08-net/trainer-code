using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.Business;
using Moq;
using Xunit;

namespace EmailApp.UnitTests
{
    public class InboxCleanerTests
    {
        [Fact]
        public void Constructor_Accepts_Null()
        {
            var cleaner = new InboxCleaner(null);
        }

        [Fact]
        public async Task CleanInbox_CleansOneSpam_WithMoq()
        {
            // arrange
            var emails = new[]
            {
                new Email { Id = 1, From = "kevin@kevin.com" },
                new Email { Id = 2, From = "a@a.com" }
            };

            var mockRepo = new Mock<IMessageRepository>();
            mockRepo.Setup(r => r.ListAsync()).ReturnsAsync(emails).Verifiable();
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<int>())).Verifiable();

            var cleaner = new InboxCleaner(mockRepo.Object);

            // act
            await cleaner.CleanInboxAsync();

            // assert
            mockRepo.Verify(r => r.ListAsync());
            mockRepo.Verify(r => r.DeleteAsync(emails[0].Id), Times.Once); // would be nice to verify it was awaited
            mockRepo.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task CleanInbox_CleansOneSpam()
        {
            // arrange
            var emails = new[]
            {
                new Email { Id = 1, From = "kevin@kevin.com" },
                new Email { Id = 2, From = "a@a.com" }
            };
            var repo = new FakeMessageRepository(emails);
            var cleaner = new InboxCleaner(repo);

            // act
            await cleaner.CleanInboxAsync();

            // assert
            Assert.Equal(emails[0].Id, repo.DeletedIds.Single());
        }

        // Moq is a library that can help replace/simplify this way of testing.

        // "test double", "mock", "fake", "stub"
        public class FakeMessageRepository : IMessageRepository
        {
            private readonly IEnumerable<Email> _emails;

            public HashSet<int> DeletedIds { get; set; } = new HashSet<int>();
            public bool Saved { get; set; } = false;

            public FakeMessageRepository(IEnumerable<Email> emails)
            {
                _emails = emails;
            }

            public Task<IEnumerable<Email>> ListAsync()
            {
                return Task.FromResult(_emails);
            }

            public Task DeleteAsync(int id)
            {
                DeletedIds.Add(id);
                return Task.CompletedTask;
            }

            public Task<Email> CreateAsync(Email email) => throw new NotImplementedException();
            public Task<Email> GetAsync(int id) => throw new NotImplementedException();
        }
    }
}
