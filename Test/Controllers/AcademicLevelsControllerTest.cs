using AutoFixture;
using EduReg.Common;
using EduReg.Controllers;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace EduReg.Tests
{
    public class AcademicLevelsControllerTest
    {
        private readonly Mock<IAcademics> _levelsMock;
        private readonly Fixture _fixture;
        private readonly AcademicsController _controller;

        public AcademicLevelsControllerTest()
        {
            _fixture = new Fixture();
            _levelsMock = new Mock<IAcademics>();

            var manager = new AcademicsManager(null!, null!, _levelsMock.Object, null!);
            _controller = new AcademicsController(manager);
        }

        private PagingParameters DefaultPaging =>
            new PagingParameters
            {
                PageNumber = 1,
                PageSize = 10
            };

        [Theory]
        [InlineData("Create")]
        [InlineData("Update")]
        [InlineData("Delete")]
        [InlineData("GetAll")]
        [InlineData("GetById")]
        public async Task ControllerMethods_ShouldReturnExpectedStatus(string method)
        {
            var id = 1;
            var response = _fixture.Build<GeneralResponse>()
                                   .With(r => r.StatusCode, 200)
                                   .Create();

            switch (method)
            {
                case "Create":
                    var dtoCreate = _fixture.Create<AcademicLevelsDto>();
                    _levelsMock.Setup(m => m.CreateAcademicLevel(dtoCreate))
                               .ReturnsAsync(response);

                    (await _controller.CreateAcademicLevelAsync(dtoCreate) as ObjectResult)!
                        .StatusCode.Should().Be(200);
                    break;

                case "Update":
                    var dtoUpdate = _fixture.Create<AcademicLevelsDto>();
                    _levelsMock.Setup(m => m.UpdateAcademicLevelAsync(id, dtoUpdate))
                               .ReturnsAsync(response);

                    (await _controller.UpdateAcademicLevelAsync(id, dtoUpdate) as ObjectResult)!
                        .StatusCode.Should().Be(200);
                    break;

                case "Delete":
                    _levelsMock.Setup(m => m.DeleteAcademicLevelAsync(id))
                               .ReturnsAsync(response);

                    (await _controller.DeleteAcademicLevelAsync(id) as ObjectResult)!
                        .StatusCode.Should().Be(200);
                    break;

                case "GetAll":
                    _levelsMock.Setup(m => m.GetAllAcademicLevelAsync(DefaultPaging))
                               .ReturnsAsync(response);

                    (await _controller.GetAllAcademicLevelsAsync(DefaultPaging) as ObjectResult)!
                        .StatusCode.Should().Be(200);
                    break;

                case "GetById":
                    _levelsMock.Setup(m => m.GetAcademicLevelByIdAsync(id))
                               .ReturnsAsync(response);

                    (await _controller.GetAcademicLevelByIdAsync(id) as ObjectResult)!
                        .StatusCode.Should().Be(200);
                    break;
                }
            }
        }
    }
