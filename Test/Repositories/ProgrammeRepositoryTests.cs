using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.Repositories
{
    public class ProgrammeRepositoryTests
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EduReg_TestDB_" + Guid.NewGuid())
                .Options;

            return new ApplicationDbContext(options);
        }


     //   [Fact]
        //public async Task CreateProgrammeAsync_AddsProgrammeToDatabase()
        //{
            
        //    using var context = CreateContext();
        //    var repo = new ProgrammesRepository(context);
        //    var dto = new ProgrammesDto
        //    {
        //        DepartmentCode = "D01",
        //        ProgrammeCode = "TEST101",
        //        ProgrammeName = "Test Programme",
        //        Description = "desc",
        //        Duration = 3,
        //        NumberOfSemesters = 6,
        //        MaximumNumberOfSemesters = 8
        //    };

            
        //    var response = await repo.CreateProgrammeAsync(dto);

            
        //    Assert.Equal(200, response.StatusCode);
        //    var saved = await context.Programmes.FirstOrDefaultAsync(p => p.ProgrammeCode == "TEST101");
        //    Assert.NotNull(saved);
        //    Assert.Equal("Test Programme", saved.ProgrammeName);
        //}

        //[Fact]
        //public async Task CreateProgrammeAsync_ReturnsConflict_WhenDuplicateCode()
        //{
        //    using var context = CreateContext();
            
        //    context.Programmes.Add(new Programmes { ProgrammeCode = "DUP001", ProgrammeName = "Existing" });
        //    await context.SaveChangesAsync();

        //    var repo = new ProgrammesRepository(context);
        //    var dto = new ProgrammesDto { ProgrammeCode = "DUP001", ProgrammeName = "Duplicate" };

        //    var response = await repo.CreateProgrammeAsync(dto);

        //    Assert.Equal(403, response.StatusCode);
        //}
    }
}
