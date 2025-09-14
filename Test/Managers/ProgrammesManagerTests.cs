using EduReg.Common;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.Managers
{
    //public class ProgrammesManagerTests
    //{
    //    [Fact]
    //    public async Task CreateProgrammeAsync_ReturnsConflict_WhenRepositoryReturnsConflict()
    //    {
    //        var mockRepo = new Mock<IProgrammes>();
    //        var dto = new ProgrammesDto { ProgrammeName = "Test Programme", ProgrammeCode = "Test101" };

    //        mockRepo.Setup(r => r.CreateProgrammeAsync(It.Is<ProgrammesDto>(d => d.ProgrammeCode == dto.ProgrammeCode)))
    //            .ReturnsAsync(new GeneralResponse { StatusCore = 403, Message = "A Programme with this Programme Code already exists" });

    //        var manager = new ProgrammesManager(mockRepo.Object);

            
    //        var result = await manager.CreateProgrammeAsync(dto);

            
    //        Assert.Equal(403, result.StatusCore);
    //        Assert.Equal("A Programme with this Programme Code already exists", result.Message);

            
    //        mockRepo.Verify(r => r.CreateProgrammeAsync(It.IsAny<ProgrammesDto>()), Times.Once);
    //    }


    //    [Fact]
    //    public async Task CreateProgrammeAsync_ReturnsSuccess_WhenRepositoryCreatesProgramme()
    //    {
            
    //        var mockRepo = new Mock<IProgrammes>();
    //        var dto = new ProgrammesDto { ProgrammeCode = "CS102", ProgrammeName = "Software Engineering" };

    //        mockRepo.Setup(r => r.CreateProgrammeAsync(It.IsAny<ProgrammesDto>()))
    //                .ReturnsAsync(new GeneralResponse { StatusCore = 200, Message = "Programme created successfully", Data = dto });

    //        var manager = new ProgrammesManager(mockRepo.Object);

            
    //        var result = await manager.CreateProgrammeAsync(dto);

            
    //        Assert.Equal(200, result.StatusCore);
    //        Assert.Equal("Programme created successfully", result.Message);
    //        Assert.Equal(dto, result.Data);
    //    }
     
    //}
}
