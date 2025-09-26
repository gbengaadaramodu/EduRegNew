using AutoFixture;
using EduReg.Common;
using EduReg.Controllers;
using EduReg.Managers;
using EduReg.Models.Dto;

using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace EduReg.Tests.Controllers
{
    public class AcademicsControllerTests
    {
        private readonly Mock<IAcademics> _levelsMock;
        private readonly AcademicsManager _manager;
        private readonly AcademicsController _controller;
        private readonly Fixture _fixture;

        public AcademicsControllerTests()
        {
            // Fixture
            _fixture = new Fixture();

            // Mock only IAcademicLevels (since that's the one we implemented)
            _levelsMock = new Mock<IAcademics>();

            // Inject mocks into AcademicsManager (other deps are null!)
            _manager = new AcademicsManager(null!, null!, _levelsMock.Object, null!);

            // Controller under test
            _controller = new AcademicsController(_manager);
        }

        [Fact]
        public async Task CreateAcademicLevelAsync_ShouldReturn201()
        {
            // Arrange
            var dto = _fixture.Create<AcademicLevelsDto>();
            _levelsMock.Setup(x => x.CreateAcademicLevel(dto))
                       .ReturnsAsync(new GeneralResponse { StatusCore = 201 });

            // Act
            var result = await _controller.CreateAcademicLevelAsync(dto);

            // Assert
            var response = result as ObjectResult;
            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task GetAcademicLevelByIdAsync_ShouldReturn200()
        {
            // Arrange
            _levelsMock.Setup(x => x.GetAcademicLevelByIdAsync(1))
                       .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

            // Act
            var result = await _controller.GetAcademicLevelByIdAsync(1);

            // Assert
            var response = result as ObjectResult;
            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllAcademicLevelsAsync_ShouldReturn200()
        {
            // Arrange
            _levelsMock.Setup(x => x.GetAllAcademicLevelAsync())
                       .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

            // Act
            var result = await _controller.GetAllAcademicLevelsAsync();

            // Assert
            var response = result as ObjectResult;
            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task UpdateAcademicLevelAsync_ShouldReturn200()
        {
            // Arrange
            var dto = _fixture.Create<AcademicLevelsDto>();
            _levelsMock.Setup(x => x.UpdateAcademicLevelAsync(1, dto))
                       .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

            // Act
            var result = await _controller.UpdateAcademicLevelAsync(1, dto);

            // Assert
            var response = result as ObjectResult;
            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteAcademicLevelAsync_ShouldReturn200()
        {
            // Arrange
            _levelsMock.Setup(x => x.DeleteAcademicLevelAsync(1))
                       .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

            // Act
            var result = await _controller.DeleteAcademicLevelAsync(1);

            // Assert
            var response = result as ObjectResult;
            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(200);
        }
    }
}
