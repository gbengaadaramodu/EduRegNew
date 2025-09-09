using EduReg.Common;
using EduReg.Controllers;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controllers
{
    public class ProgrammesControllerTests
    {
        private readonly Mock<IProgrammes> _mockRepo;
        private readonly ProgrammesManager _manager;
        private readonly Mock<ILogger<ProgrammesController>> _mockLogger;

        public ProgrammesControllerTests()
        {
            _mockRepo = new Mock<IProgrammes>();
            _manager = new ProgrammesManager(_mockRepo.Object);
            _mockLogger = new Mock<ILogger<ProgrammesController>>();
        }

        [Fact]
        public async Task CreateProgramme_ReturnsConflict_WhenProgrammeExists()
        {
            
            var dto = new ProgrammesDto { ProgrammeCode = "BSc", ProgrammeName = "Bachelors" };
            _mockRepo.Setup(r => r.CreateProgrammeAsync(It.IsAny<ProgrammesDto>()))
                     .ReturnsAsync(new GeneralResponse { StatusCore = 403, Message = "A Programme with this Programme Code already exists" });

            var controller = new ProgrammesController(_manager, _mockLogger.Object);

            
            var result = await controller.CreateProgramme(dto);

            
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(conflictResult.Value);
            Assert.Equal(403, response.StatusCore);
        }


        [Fact]
        public async Task CreateProgramme_ReturnsBadRequest_WhenModelStateInvalid()
        {
            
            var dto = new ProgrammesDto(); 
            var controller = new ProgrammesController(_manager, _mockLogger.Object);
            controller.ModelState.AddModelError("ProgrammeCode", "Required");

            
            var result = await controller.CreateProgramme(dto);

            
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProgramme_ReturnsNotFound_WhenProgrammeMissing()
        {
            
            _mockRepo.Setup(r => r.DeleteProgrammeAsync(It.IsAny<int>()))
                     .ReturnsAsync(new GeneralResponse { StatusCore = 404, Message = "Programme not found" });

            var controller = new ProgrammesController(_manager, _mockLogger.Object);

            
            var result = await controller.DeleteProgramme(123);

            
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}

