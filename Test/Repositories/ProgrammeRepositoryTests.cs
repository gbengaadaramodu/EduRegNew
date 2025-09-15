using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using System.Linq;

namespace EduReg.Tests.Repositories
{
    public class ProgrammesRepositoryTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"EduRegTestDb_{System.Guid.NewGuid()}")
                .Options;

            return new ApplicationDbContext(options);
        }


        [Fact]
        public async Task CreateProgrammeAsync_ShouldReturn200_WhenProgrammeIsNew()
        {
            
            var context = GetDbContext();
            var repo = new ProgrammesRepository(context);

            var dto = new ProgrammesDto
            {
                DepartmentCode = "CS",
                ProgrammeCode = "CS101",
                ProgrammeName = "Computer Science",
                Description = "BSc in CS",
                Duration = 4,
                NumberOfSemesters = 8,
                MaximumNumberOfSemesters = 10
            };

            
            var response = await repo.CreateProgrammeAsync(dto);

            
            Assert.Equal(200, response.StatusCore);
            Assert.Equal("Programme created successfully", response.Message);
            Assert.NotNull(response.Data);

            var dbProgramme = await context.Programmes.FirstOrDefaultAsync();
            Assert.NotNull(dbProgramme);
            Assert.Equal("CS101", dbProgramme.ProgrammeCode);
        }



        [Fact]
        public async Task CreateProgrammeAsync_ShouldReturn403_WhenProgrammeCodeAlreadyExists()
        {
         
            var context = GetDbContext();
            await context.Programmes.AddAsync(new Programmes
            {
                DepartmentCode = "CS",
                ProgrammeCode = "CS101",
                ProgrammeName = "Existing CS",
                Description = "Already in DB",
                Duration = 4,
                NumberOfSemesters = 8,
                MaximumNumberOfSemesters = 10
            });
            await context.SaveChangesAsync();

            var repo = new ProgrammesRepository(context);

            var dto = new ProgrammesDto
            {
                DepartmentCode = "CS",
                ProgrammeCode = "CS101",
                ProgrammeName = "Computer Science",
                Description = "Duplicate",
                Duration = 4,
                NumberOfSemesters = 8,
                MaximumNumberOfSemesters = 10
            };

            
            var response = await repo.CreateProgrammeAsync(dto);

            
            Assert.Equal(403, response.StatusCore);
            Assert.Equal("A Programme with this Programme Code already exists", response.Message);
        }

        [Fact]
        public async Task DeleteProgrammeAsync_ShouldReturn200_WhenProgrammeExists()
        {
            
            var context = GetDbContext();
            var programme = new Programmes
            {
                DepartmentCode = "ENG",
                ProgrammeCode = "ENG201",
                ProgrammeName = "English Studies",
                Description = "BA in English",
                Duration = 4,
                NumberOfSemesters = 8,
                MaximumNumberOfSemesters = 10
            };

            await context.Programmes.AddAsync(programme);
            await context.SaveChangesAsync();

            var repo = new ProgrammesRepository(context);

            
            var response = await repo.DeleteProgrammeAsync(programme.Id);

            
            Assert.Equal(200, response.StatusCore);
            Assert.Equal("Programme deleted successfully", response.Message);

            var deleted = await context.Programmes.FindAsync(programme.Id);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task DeleteProgrammeAsync_ShouldReturn404_WhenProgrammeNotFound()
        {
            
            var context = GetDbContext();
            var repo = new ProgrammesRepository(context);

            
            var response = await repo.DeleteProgrammeAsync(999);

            
            Assert.Equal(404, response.StatusCore);
            Assert.Equal("Programme not found", response.Message);
        }

        [Fact]
        public async Task GetAllProgrammesAsync_ShouldReturn200_WithListOfProgrammes()
        {
            
            var context = GetDbContext();
            await context.Programmes.AddAsync(new Programmes
            {
                DepartmentCode = "MATH",
                ProgrammeCode = "MATH101",
                ProgrammeName = "Mathematics",
                Description = "BSc in Math",
                Duration = 4,
                NumberOfSemesters = 8,
                MaximumNumberOfSemesters = 10
            });
            await context.SaveChangesAsync();

            var repo = new ProgrammesRepository(context);

            
            var response = await repo.GetAllProgrammesAsync();

            
            Assert.Equal(200, response.StatusCore);
            Assert.NotNull(response.Data);
            var programmes = response.Data as List<ProgrammesDto>;
            Assert.Single(programmes);
            Assert.Equal("Mathematics", programmes.First().ProgrammeName);
        }
    }
}
