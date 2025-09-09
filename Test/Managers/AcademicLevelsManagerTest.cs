using AutoFixture;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using EduReg.Common;

namespace EduReg.Tests
{
    public class AcademicLevelsManagerTest
    {
        private readonly Mock<IAcademicLevels> _levelsMock;
        private readonly AcademicsManager _manager;
        private readonly Fixture _fixture;

        public AcademicLevelsManagerTest()
        {
            _levelsMock = new Mock<IAcademicLevels>();
         
            _manager = new AcademicsManager(null!, null!, _levelsMock.Object, null!);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task CreateAcademicLevelAsync_ShouldReturnExpectedResponse()
        {
           
            var dto = _fixture.Create<AcademicLevelsDto>();
            var response = _fixture.Build<GeneralResponse>()
                                   .With(r => r.StatusCore, 201)
                                   .Create();

            _levelsMock.Setup(l => l.CreateAcademicLevel(dto))
                       .ReturnsAsync(response);

         
            var result = await _manager.CreateAcademicLevelAsync(dto);

          
            result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task UpdateAcademicLevelAsync_ShouldCallLevelsAndReturnResponse()
        {
            var id = 1;
            var dto = _fixture.Create<AcademicLevelsDto>();
            var response = _fixture.Build<GeneralResponse>()
                                   .With(r => r.StatusCore, 200)
                                   .Create();

            _levelsMock.Setup(l => l.UpdateAcademicLevelAsync(id, dto))
                       .ReturnsAsync(response);

            var result = await _manager.UpdateAcademicLevelAsync(id, dto);

            result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task DeleteAcademicLevelAsync_ShouldReturnExpectedResponse()
        {
            var id = 1;
            var response = _fixture.Build<GeneralResponse>()
                                   .With(r => r.StatusCore, 204)
                                   .Create();

            _levelsMock.Setup(l => l.DeleteAcademicLevelAsync(id))
                       .ReturnsAsync(response);

            var result = await _manager.DeleteAcademicLevelAsync(id);

            result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task GetAcademicLevelByIdAsync_ShouldReturnExpectedResponse()
        {
            var id = 1;
            var response = _fixture.Build<GeneralResponse>()
                                   .With(r => r.StatusCore, 200)
                                   .Create();

            _levelsMock.Setup(l => l.GetAcademicLevelByIdAsync(id))
                       .ReturnsAsync(response);

            var result = await _manager.GetAcademicLevelByIdAsync(id);

            result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task GetAllAcademicLevelsAsync_ShouldReturnExpectedResponse()
        {
            var response = _fixture.Build<GeneralResponse>()
                                   .With(r => r.StatusCore, 200)
                                   .Create();

            _levelsMock.Setup(l => l.GetAllAcademicLevelAsync())
                       .ReturnsAsync(response);

            var result = await _manager.GetAllAcademicLevelsAsync();

            result.Should().BeEquivalentTo(response);
        }
    }
}
