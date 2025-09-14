//using AutoFixture;
//using EduReg.Common;
//using EduReg.Controllers;
//using EduReg.Managers;
//using EduReg.Models.Dto;
//using EduReg.Services.Interfaces;
//using FluentAssertions;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Threading.Tasks;
//using Xunit;

//namespace EduReg.Tests
//{
//    public class AcademicLevelsControllerTest
//    {
//        private readonly Mock<IAcademicLevels> _levelsMock;
//        private readonly Fixture _fixture;
//        private readonly AcademicLevelController _controller;

//        public AcademicLevelsControllerTest()
//        {
//            _fixture = new Fixture();

      
//            _levelsMock = new Mock<IAcademicLevels>();

        
//            var manager = new AcademicsManager(null!, null!, _levelsMock.Object, null!);

            
//         //   _controller = new AcademicLevelController(manager);
//        }

//        [Theory]
//        [InlineData("Create")]
//        [InlineData("Update")]
//        [InlineData("Delete")]
//        [InlineData("GetAll")]
//        [InlineData("GetById")]
//        public async Task ControllerMethods_ShouldReturnExpectedStatus(string method)
//        {
//            var id = 1;
//            var response = _fixture.Build<GeneralResponse>()
//                                   .With(r => r.StatusCore, 200)
//                                   .Create();

//            switch (method)
//            {
//                case "Create":
//                    var dtoCreate = _fixture.Create<AcademicLevelsDto>();
//                    _levelsMock.Setup(m => m.CreateAcademicLevel(dtoCreate))
//                               .ReturnsAsync(response);
//                    (await _controller.CreateAcademicLevel(dtoCreate) as ObjectResult)!
//                        .StatusCode.Should().Be(200);
//                    break;

//                case "Update":
//                    var dtoUpdate = _fixture.Create<AcademicLevelsDto>();
//                    _levelsMock.Setup(m => m.UpdateAcademicLevelAsync(id, dtoUpdate))
//                               .ReturnsAsync(response);
//                    (await _controller.UpdateAcademicLevel(id, dtoUpdate) as ObjectResult)!
//                        .StatusCode.Should().Be(200);
//                    break;

//                case "Delete":
//                    _levelsMock.Setup(m => m.DeleteAcademicLevelAsync(id))
//                               .ReturnsAsync(response);
//                    (await _controller.DeleteAcademicLevel(id) as ObjectResult)!
//                        .StatusCode.Should().Be(200);
//                    break;

//                case "GetAll":
//                    _levelsMock.Setup(m => m.GetAllAcademicLevelAsync())
//                               .ReturnsAsync(response);
//                    (await _controller.GetAllAcademicLevels() as ObjectResult)!
//                        .StatusCode.Should().Be(200);
//                    break;

//                case "GetById":
//                    _levelsMock.Setup(m => m.GetAcademicLevelByIdAsync(id))
//                               .ReturnsAsync(response);
//                    (await _controller.GetAcademicLevelById(id) as ObjectResult)!
//                        .StatusCode.Should().Be(200);
//                    break;
//            }
//        }
//    }
//}
