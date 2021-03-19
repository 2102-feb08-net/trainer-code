using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.WebUI.Controllers;
using EmailApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmailApp.UnitTests
{
    public class EmailControllerTests
    {
        [Fact]
        public async Task SendMessage_ValidMessage_AddsToRepo()
        {
            // arrange
            Message toSend = new()
            {
                From = "kevin@kevin.com"
            };
            Email created = new()
            {
                Id = 3,
                From = "kevin@kevin.com"
            };
            List<Email> results = new();
            var mockRepo = new Mock<IMessageRepository>();
            mockRepo.Setup(r => r.CreateAsync(Capture.In(results))).ReturnsAsync(created).Verifiable();
            var mockCleaner = new Mock<IInboxCleaner>();
            var mockTime = new Mock<ITimeProvider>();
            var controller = new EmailController(mockRepo.Object, mockCleaner.Object, mockTime.Object);

            // act
            IActionResult result = await controller.SendMessage(toSend, null);

            // assert
            mockRepo.Verify(r => r.CreateAsync(It.IsAny<Email>()), Times.Once);
            var argument = results.Single();
            Assert.Equal(expected: toSend.From, actual: argument.From);

            var createdResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            Assert.Equal(expected: nameof(controller.GetMessage), actual: createdResult.ActionName);
            // TODO assert on createdResult.RouteValues that it has id = 3
            // TODO maybe check the response body / result-contained object that it's correct
        }
    }
}
