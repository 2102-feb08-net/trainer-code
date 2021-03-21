using System;
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
            _ = new InboxCleaner(null);
        }

        [Fact]
        public async Task CleanInbox_CleansOneSpam()
        {
            // arrange
            var emails = new[]
            {
                new Email { Id = Guid.NewGuid(), From = "kevin@kevin.com" },
                new Email { Id = Guid.NewGuid(), From = "a@a.com" }
            };

            var mockRepo = new Mock<IMessageRepository>();
            mockRepo.Setup(r => r.ListAsync()).ReturnsAsync(emails).Verifiable();
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).Verifiable();

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(u => u.MessageRepository).Returns(mockRepo.Object);

            var cleaner = new InboxCleaner(mockUow.Object);

            // act
            await cleaner.CleanInboxAsync();

            // assert
            mockRepo.Verify(r => r.ListAsync());
            mockRepo.Verify(r => r.DeleteAsync(emails[0].Id), Times.Once); // would be nice to verify it was awaited
            mockRepo.VerifyNoOtherCalls();
            mockUow.Verify(u => u.SaveAsync());
        }
    }
}
