using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmailApp.Business;
using EmailApp.WebUI.Controllers;
using EmailApp.WebUI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmailApp.UnitTests
{
    public class MailControllerTests
    {
        [Fact]
        public async Task SendMessage_ValidMessage_AddsToRepoAndSaves()
        {
            // arrange
            MessageInput toSend = new()
            {
                From = "kevin@kevin.com",
                To = "a@a.com",
                Date = new DateTime(2021, 1, 1)
            };
            Email created = new()
            {
                Id = Guid.NewGuid(),
                From = toSend.From,
                To = toSend.To,
                OrigDate = (DateTimeOffset)toSend.Date
            };
            List<Email> results = new();
            var mockAuth = new Mock<IAuthorizationService>();
            mockAuth.Setup(s => s.AuthorizeAsync(
                It.IsAny<ClaimsPrincipal>(), It.IsAny<object>(), It.IsAny<string>()))
                .ReturnsAsync(AuthorizationResult.Success());
            var mockRepo = new Mock<IMessageRepository>();
            mockRepo.Setup(r => r.AddAsync(Capture.In(results))).ReturnsAsync(created);
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(r => r.MessageRepository).Returns(mockRepo.Object);
            var controller = new MailController(mockAuth.Object, mockUow.Object);

            // act
            IActionResult result = await controller.SendMessage(toSend);

            // assert
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Email>()), Times.Once);
            mockRepo.VerifyNoOtherCalls();
            var argument = results.Single();
            Assert.Equal(expected: toSend.From, actual: argument.From);
            mockUow.Verify(u => u.SaveAsync(), Times.Once);
            mockUow.VerifyNoOtherCalls();

            var createdResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            Assert.Equal(expected: nameof(controller.GetMessage), actual: createdResult.ActionName);
            // TODO assert on createdResult.RouteValues that it has id = created.Id
            // TODO maybe check the response body / result-contained object that it's correct
        }
    }
}
