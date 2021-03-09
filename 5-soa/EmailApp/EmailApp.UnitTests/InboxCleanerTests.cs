using System;
using System.Collections.Generic;
using System.Linq;
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
        public void CleanInbox_CleansOneSpam_WithMoq()
        {
            // arrange
            var emails = new[]
            {
                new Email { Id = 1, From = "kevin@kevin.com" },
                new Email { Id = 2, From = "a@a.com" }
            };

            var mockRepo = new Mock<IMessageRepository>();
            mockRepo.Setup(r => r.List()).Returns(emails).Verifiable();
            mockRepo.Setup(r => r.Delete(It.IsAny<int>())).Verifiable();
            mockRepo.Setup(r => r.Save()).Verifiable();

            var cleaner = new InboxCleaner(mockRepo.Object);

            // act
            cleaner.CleanInbox();

            // assert
            mockRepo.Verify(r => r.List());
            mockRepo.Verify(r => r.Delete(emails[0].Id), Times.Once);
            mockRepo.Verify(r => r.Save());
            mockRepo.VerifyNoOtherCalls();
        }

        [Fact]
        public void CleanInbox_CleansOneSpam()
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
            cleaner.CleanInbox();

            // assert
            Assert.Equal(emails[0].Id, repo.DeletedIds.Single());
            Assert.True(repo.Saved);
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

            public IEnumerable<Email> List()
            {
                return _emails;
            }

            public void Delete(int id)
            {
                DeletedIds.Add(id);
            }

            public void Save()
            {
                Saved = true;
            }

            public void Create(Email email) => throw new NotImplementedException();
            public Email Get(int id) => throw new NotImplementedException();
        }
    }
}
