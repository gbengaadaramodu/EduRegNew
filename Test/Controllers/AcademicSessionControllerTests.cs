using EduReg.Common;
using EduReg.Controllers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controllers
{
    public class AcademicSessionControllerTests
    {
        [Fact]
        public async Task GetSessions_ReturnsOkResult()
        {
            // Arrange
            var mockRepo = new Mock<IAcademicSessions>();
            mockRepo.Setup(m => m.GetAllAcademicSessionsAsync())
                       .ReturnsAsync(new GeneralResponse { Data = new List<AcademicSessionsDto>(), Message = "Success", StatusCore = 200 });
            var controller = new AcademicSessionsController(mockRepo.Object);
            // Act
            var result = await controller.GetAllAcademicSessions();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);
            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal(200, response.StatusCore);
            Assert.Equal("Success", response.Message);
        }

        [Fact]
        public async Task GetSessionById_ReturnsOkResult()
        {
            var mockRepo = new Mock<IAcademicSessions>();
            var sessionId = 1;
            mockRepo.Setup(m => m.GetAcademicSessionByIdAsync(sessionId))
                       .ReturnsAsync(new GeneralResponse { Data = new AcademicSessionsDto { SessionId = sessionId, SessionName = "2023/2024 session" }, Message = "Success", StatusCore = 200 });
            var controller = new AcademicSessionsController(mockRepo.Object);

            var result = await controller.GetAcademicSessionById(sessionId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);

            Assert.NotNull(response.Data);
            Assert.Equal(sessionId, ((AcademicSessionsDto)response.Data).SessionId);
            Assert.Equal(200, response.StatusCore);
            Assert.Equal("Success", response.Message);
        }

        [Fact]
        public async Task GetSessionById_Returns404WithWrongId()
        {
            var mockRepo = new Mock<IAcademicSessions>();
            mockRepo.Setup(m => m.GetAcademicSessionByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync(new GeneralResponse { Data = null, Message = "Not Found", StatusCore = 404 });
            var controller = new AcademicSessionsController(mockRepo.Object);
            int wrongId = 999;
            var result = await controller.GetAcademicSessionById(wrongId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);
            Assert.Null(response.Data);
            Assert.Equal(404, response.StatusCore);
            Assert.Equal("Not Found", response.Message);
        }

        [Fact]
        public async Task GetSessions_HandlesException()
        {
            // Arrange
            var mockRepo = new Mock<IAcademicSessions>();
            mockRepo.Setup(m => m.GetAllAcademicSessionsAsync())
                       .ThrowsAsync(new Exception("Test exception"));
            var controller = new AcademicSessionsController(mockRepo.Object);
            // Act
            var result = await controller.GetAllAcademicSessions();
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(badRequestResult.Value);
            // Assert
            Assert.Null(response.Data);
            Assert.Equal(400, response.StatusCore);
            Assert.Equal("Test exception", response.Message);
        }

        [Fact]
        public async Task GetSessionById_HandlesException()
        {
            var mockRepo = new Mock<IAcademicSessions>();
            var sessionId = 1;
            mockRepo.Setup(m => m.GetAcademicSessionByIdAsync(sessionId))
                       .ThrowsAsync(new Exception("Test exception"));
            var controller = new AcademicSessionsController(mockRepo.Object);
            var result = await controller.GetAcademicSessionById(sessionId);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(badRequestResult.Value);
            Assert.Null(response.Data);
            Assert.Equal(400, response.StatusCore);
            Assert.Equal("Test exception", response.Message);
        }

        [Fact]
        public async Task GetSessionById_HandlesExceptionWithEmptyMessage()
        {
            var mockRepo = new Mock<IAcademicSessions>();
            var sessionId = 1;
            mockRepo.Setup(m => m.GetAcademicSessionByIdAsync(sessionId))
                       .ThrowsAsync(new Exception());
            var controller = new AcademicSessionsController(mockRepo.Object);
            var result = await controller.GetAcademicSessionById(sessionId);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(badRequestResult.Value);
            Assert.Null(response.Data);
            Assert.Equal(400, response.StatusCore);
            Assert.Equal("An Error occurred, please try again after some time.", response.Message);
        }

        [Fact]
        public async Task GetSessions_HandlesExceptionWithEmptyMessage()
        {
            // Arrange
            var mockRepo = new Mock<IAcademicSessions>();
            mockRepo.Setup(m => m.GetAllAcademicSessionsAsync())
                       .ThrowsAsync(new Exception());
            var controller = new AcademicSessionsController(mockRepo.Object);
            // Act
            var result = await controller.GetAllAcademicSessions();
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(badRequestResult.Value);
            // Assert
            Assert.Null(response.Data);
            Assert.Equal(400, response.StatusCore);
            Assert.Equal("An Error occurred, please try again after some time.", response.Message);
        }

        [Fact]
        public async Task DeleteSessionById_ReturnsOkResult()
        {
            var mockRepo = new Mock<IAcademicSessions>();
            var sessionId = 1;
            mockRepo.Setup(m => m.DeleteAcademicSessionAsync(sessionId))
                       .ReturnsAsync(new GeneralResponse { Data = null, Message = "Deleted Successfully", StatusCore = 200 });
            var controller = new AcademicSessionsController(mockRepo.Object);

            var result = await controller.DeleteAcademicSession(sessionId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);

            Assert.Null(response.Data);
            Assert.Equal(200, response.StatusCore);
            Assert.Equal("Deleted Successfully", response.Message);
        }

        [Fact]
        public async Task DeleteSessionById_Returns404WithWrongId()
        {
            var mockRepo = new Mock<IAcademicSessions>();
            mockRepo.Setup(m => m.DeleteAcademicSessionAsync(It.IsAny<int>()))
                       .ReturnsAsync(new GeneralResponse { Data = null, Message = "Not Found", StatusCore = 404 });
            var controller = new AcademicSessionsController(mockRepo.Object);
            int wrongId = 999;
            var result = await controller.DeleteAcademicSession(wrongId);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(notFoundResult.Value);
            Assert.Null(response.Data);
            Assert.Equal(404, response.StatusCore);
            Assert.Equal("Not Found", response.Message);
        }

        [Fact]
        public async Task DeleteSessionById_HandlesException()
        {
            var mockRepo = new Mock<IAcademicSessions>();
            var sessionId = 1;
            mockRepo.Setup(m => m.DeleteAcademicSessionAsync(sessionId))
                       .ThrowsAsync(new Exception("Test exception"));
            var controller = new AcademicSessionsController(mockRepo.Object);
            var result = await controller.DeleteAcademicSession(sessionId);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(badRequestResult.Value);
            Assert.Null(response.Data);
            Assert.Equal(400, response.StatusCore);
            Assert.Equal("Test exception", response.Message);
        }

        [Fact]
        public async Task CreateSession_ReturnsOkResult()
        {
            var mockRepo = new Mock<IAcademicSessions>();
            var newSession = new AcademicSessionsDto { SessionName = "New Session" };
            mockRepo.Setup(m => m.CreateAcademicSessionAsync(newSession))
                       .ReturnsAsync(new GeneralResponse { Data = new AcademicSessionsDto { SessionId = 1, SessionName = "New Session" }, Message = "Created Successfully", StatusCore = 201 });
            var controller = new AcademicSessionsController(mockRepo.Object);
            var result = await controller.CreateAcademicSession(newSession);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);
            Assert.NotNull(response.Data);
            Assert.Equal(201, response.StatusCore);
            Assert.Equal("Created Successfully", response.Message);
        }

        [Fact]
        public async Task CreateSession_ReturnsBadRequest_OnException()
        {
            var mockRepo = new Mock<IAcademicSessions>();
            var newSession = new AcademicSessionsDto { SessionName = "New Session" };
            mockRepo.Setup(m => m.CreateAcademicSessionAsync(newSession))
                       .ThrowsAsync(new Exception("Test exception"));
            var controller = new AcademicSessionsController(mockRepo.Object);
            var result = await controller.CreateAcademicSession(newSession);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(badRequestResult.Value);
            Assert.Null(response.Data);
            Assert.Equal(400, response.StatusCore);
            Assert.Equal("Test exception", response.Message);
        }
    }
}
