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
            var address = "a@a.com";
            var emails = new[]
            {
                new Email { Id = Guid.NewGuid(), From = "kevin@kevin.com", To = address },
                new Email { Id = Guid.NewGuid(), From = "b@b.com", To = address }
            };

            var mockRepo = new Mock<IMessageRepository>();
            mockRepo.Setup(r => r.ListByRecipientAsync(address)).ReturnsAsync(emails);
            mockRepo.Setup(r => r.DeleteByIdAsync(It.IsAny<Guid>()));

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(u => u.MessageRepository).Returns(mockRepo.Object);

            var cleaner = new InboxCleaner(mockUow.Object);

            // act
            await cleaner.CleanInboxAsync(address);

            // assert
            mockRepo.Verify(r => r.ListByRecipientAsync(address), Times.Once);
            mockRepo.Verify(r => r.DeleteByIdAsync(emails[0].Id), Times.Once); // would be nice to verify it was awaited
            mockRepo.VerifyNoOtherCalls();
            mockUow.Verify(u => u.SaveAsync());
            mockUow.VerifyNoOtherCalls();
        }
    }
}
