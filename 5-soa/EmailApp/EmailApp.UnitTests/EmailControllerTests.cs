using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.WebUI.Controllers;
using EmailApp.WebUI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmailApp.UnitTests
{
    public class EmailControllerTests
    {
        [Fact]
        public async Task SendMessage_ValidMessage_AddsToRepoAndSaves()
        {
            // arrange
            MessageInput toSend = new()
            {
                From = "kevin@kevin.com",
                Date = new DateTime(2021, 1, 1)
            };
            Email created = new()
            {
                Id = Guid.NewGuid(),
                From = toSend.From,
                OrigDate = (DateTimeOffset)toSend.Date
            };
            List<Email> results = new();
            var mockRepo = new Mock<IMessageRepository>();
            mockRepo.Setup(r => r.CreateAsync(Capture.In(results))).ReturnsAsync(created).Verifiable();
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(r => r.MessageRepository).Returns(mockRepo.Object);
            var controller = new MailController(mockUow.Object);

            // act
            IActionResult result = await controller.SendMessage(toSend);

            // assert
            mockRepo.Verify(r => r.CreateAsync(It.IsAny<Email>()), Times.Once);
            var argument = results.Single();
            Assert.Equal(expected: toSend.From, actual: argument.From);
            mockUow.Verify(u => u.SaveAsync(), Times.Once);

            var createdResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            Assert.Equal(expected: nameof(controller.GetMessage), actual: createdResult.ActionName);
            // TODO assert on createdResult.RouteValues that it has id = created.Id
            // TODO maybe check the response body / result-contained object that it's correct
        }
    }
}
