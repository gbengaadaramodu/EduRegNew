using EduReg.Common;
using EduReg.Controllers;
using EduReg.Data;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test.Controllers
{
    public class SemesterControllerTests
    {

        [Fact]
        public async Task GetSemesters_ReturnsOkResult()
        {
            // Arrange
            var mockRepo = new Mock<ISemesters>();
            mockRepo.Setup(m => m.GetAllSemestersAsync())
                       .ReturnsAsync(new GeneralResponse { Data = new List<SemestersDto>(), Message = "Success", StatusCore = 200 });
            var controller = new SemestersController(mockRepo.Object);

            // Act
            var result = await controller.GetAllSemesters();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);

            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal(200, response.StatusCore);
            Assert.Equal("Success", response.Message); 
        }

        [Fact]
        public async Task GetSemesterById_ReturnsOkResult()
        {
            var mockRepo = new Mock<ISemesters>();
            var semesterId = 1;
            mockRepo.Setup(m => m.GetSemesterByIdAsync(semesterId))
                       .ReturnsAsync(new GeneralResponse { Data = new SemestersDto { SemesterId = semesterId, SemesterName = "Alpha semester" }, Message = "Success", StatusCore = 200 });
            var controller = new SemestersController(mockRepo.Object);

            var result = await controller.GetSemesterById(semesterId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);

            Assert.NotNull(response.Data);
            Assert.Equal(semesterId, ((SemestersDto)response.Data).SemesterId);
            Assert.Equal(200, response.StatusCore);
            Assert.Equal("Success", response.Message);
        }

        [Fact]
        public async Task GetSemesterById_Returns404WithWrongId()
        {
            var mockRepo = new Mock<ISemesters>();
            mockRepo.Setup(m => m.GetSemesterByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync(new GeneralResponse { Data = null, Message = "Not Found", StatusCore = 404 });
            var controller = new SemestersController(mockRepo.Object);
            int wrongId = 999;

            var result = await controller.GetSemesterById(wrongId);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(notFoundResult.Value);

            Assert.Null(response.Data);
            Assert.Equal(404, response.StatusCore);
            Assert.Equal("Not Found", response.Message);
        }

        [Fact]
        public async Task DeleteSemesterById_ReturnsOkResult()
        {
            var mockRepo = new Mock<ISemesters>();
            var semesterId = 1;
            mockRepo.Setup(m => m.DeleteSemesterAsync(semesterId))
                       .ReturnsAsync(new GeneralResponse { Data = null, Message = "Semester deleted Successfully", StatusCore = 200 });

            var controller = new SemestersController(mockRepo.Object);
            var result = await controller.DeleteSemester(semesterId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);

            Assert.Null(response.Data);
            Assert.Equal(200, response.StatusCore);
            Assert.Equal("Semester deleted Successfully", response.Message);
        }

        [Fact]
        public async Task DeleteSemesterById_Returns404WithWrongId()
        {
            var mockRepo = new Mock<ISemesters>();
            mockRepo.Setup(m => m.DeleteSemesterAsync(It.IsAny<int>()))
                       .ReturnsAsync(new GeneralResponse { Data = null, Message = "Semester not found", StatusCore = 404 });
            var controller = new SemestersController(mockRepo.Object);
            int wrongId = 999;

            var result = await controller.DeleteSemester(wrongId);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(notFoundResult.Value);

            Assert.Null(response.Data);
            Assert.Equal(404, response.StatusCore);
            Assert.Equal("Semester not found", response.Message);
        }

        [Fact]
        public async Task CreateSemester_ReturnsOkResult()
        {
            var mockRepo = new Mock<ISemesters>();
            var newSemester = new SemestersDto
            {
                SemesterId = 1,
                SemesterName = "Alpha semester",
                SessionId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4)
            };
            mockRepo.Setup(m => m.CreateSemesterAsync(newSemester))
                       .ReturnsAsync(new GeneralResponse { Data = newSemester, Message = "Semester created successfully", StatusCore = 200 });
            var controller = new SemestersController(mockRepo.Object);
            
            var result = await controller.CreateSemester(newSemester);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);

            Assert.NotNull(response.Data);
            Assert.Equal(200, response.StatusCore);
            Assert.Equal("Semester created successfully", response.Message);
        }

        [Fact]
        public async Task CreateSemester_ReturnsBadRequest_OnException()
        {
            var mockRepo = new Mock<ISemesters>();
            var newSemester = new SemestersDto
            {
                SemesterId = 1,
                SemesterName = "Alpha semester",
                SessionId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4)
            };
            mockRepo.Setup(m => m.CreateSemesterAsync(newSemester))
                       .ThrowsAsync(new Exception("Database error"));
            var controller = new SemestersController(mockRepo.Object);
            var result = await controller.CreateSemester(newSemester);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(badRequestResult.Value);
            Assert.Null(response.Data);
            Assert.Equal(400, response.StatusCore);
            Assert.Equal("Database error", response.Message);
        }

        [Fact]
        public async Task CreateSemester_ReturnsNotFound_WithWrongSessionId()
        {
            var mockSemester = new Mock<ISemesters>();
            var mockSession = new Mock<IAcademicSessions>();

            mockSemester.Setup(m => m.CreateSemesterAsync(It.IsAny<SemestersDto>()))
                       .ReturnsAsync(new GeneralResponse { Data = null, Message = "Session does not exist", StatusCore = 404 });
            
            var controller = new SemestersController(mockSemester.Object);
            var model = new SemestersDto { SessionId = 999, SemesterId = 1, SemesterName = "Alpha semester", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(4) };

            var result = await controller.CreateSemester(model);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(notFoundResult.Value);

            Assert.Equal(404, response.StatusCore);
            Assert.Equal("Session does not exist", response.Message);
        }
    }
}
