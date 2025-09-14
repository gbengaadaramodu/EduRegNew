using EduReg.Common;
using EduReg.Controllers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;   // contains IFaculties
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace EduReg.Tests.Controllers
{
    public class FacultiesControllerTests
    {
        private readonly Mock<IFaculties> _mockService;
        private readonly FacultiesController _controller;

        public FacultiesControllerTests()
        {
            // Mock the IFaculties service
            _mockService = new Mock<IFaculties>();

            // Inject the mock into the controller
            _controller = new FacultiesController(_mockService.Object);
        }

        [Fact]
        public async Task CreateFaculty_ReturnsOk_WhenStatus200()
        {
            // Arrange
            var dto = new FacultiesDto { FacultyName = "Engineering" };
            var response = new GeneralResponse { StatusCore = 200, Message = "Created" };

            _mockService.Setup(s => s.CreateFacultyAsync(dto))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateFaculty(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GeneralResponse>(okResult.Value);
            Assert.Equal(200, returnValue.StatusCore);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenFacultyDoesNotExist()
        {
            // Arrange
            int facultyId = 1;
            var response = new GeneralResponse { StatusCore = 404, Message = "Not Found" };

            _mockService.Setup(s => s.GetFacultyByIdAsync(facultyId))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.GetById(facultyId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenInvalidData()
        {
            // Arrange
            int facultyId = 2;
            var dto = new FacultiesDto { FacultyName = "" };
            var response = new GeneralResponse { StatusCore = 400, Message = "Bad Request" };

            _mockService.Setup(s => s.UpdateFacultyAsync(facultyId, dto))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.Update(facultyId, dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFaculty_ReturnsOk_WhenDeleted()
        {
            // Arrange
            int facultyId = 3;
            var response = new GeneralResponse { StatusCore = 200, Message = "Deleted" };

            _mockService.Setup(s => s.DeleteFacultyAsync(facultyId))
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.DeleteFacultyAsync(facultyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GeneralResponse>(okResult.Value);
            Assert.Equal("Deleted", returnValue.Message);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WhenFacultiesExist()
        {
            // Arrange
            var response = new GeneralResponse { StatusCore = 200, Message = "Success" };

            _mockService.Setup(s => s.GetAllFacultiesAsync())
                        .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GeneralResponse>(okResult.Value);
            Assert.Equal("Success", returnValue.Message);
        }
    }
}
