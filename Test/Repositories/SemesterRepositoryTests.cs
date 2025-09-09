using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using EduReg.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repositories
{
    public class SemesterRepositoryTests
    {
    //    private ApplicationDbContext GetDbContext()
    //    {
    //        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    //            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
    //            .Options;
    //        var dbContext = new ApplicationDbContext(options);
    //        return dbContext;
    //    }
    //    [Fact]
    //    public async Task GetAllSemesters_ReturnsSuccessResponse()
    //    {
    //        // Arrange
    //        var mockRepo = new Mock<ISemesters>();
    //        mockRepo.Setup(m => m.GetAllSemestersAsync())
    //                   .ReturnsAsync(new GeneralResponse { Data = new List<SemestersDto>(), Message = "Success", StatusCore = 200 });

    //        var service = new SemestersRepository(mockRepo.Object);
    //    }
    }
}
