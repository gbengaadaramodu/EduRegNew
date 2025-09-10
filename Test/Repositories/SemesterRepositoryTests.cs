using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
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
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);

            context.Semesters.AddRange(
                new Semester { Id = 1, SemesterName = "Fall 2023" },
                new Semester { Id = 2, SemesterName = "Spring 2024" }
            );
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetAllSemesters_ReturnsSuccessResponse()
        {
            var context = GetDbContext();
            var service = new SemestersRepository(context);

            var result = await service.GetAllSemestersAsync();

            Assert.Equal(200, result.StatusCore);
            Assert.NotNull(result.Data);
            Assert.Equal("Successful", result.Message);
        }

        [Fact]
        public async Task GetSemesterById_ExistingId_ReturnsSuccessResponse()
        {
            var context = GetDbContext();
            var service = new SemestersRepository(context);
            var result = await service.GetSemesterByIdAsync(1);
            Assert.Equal(200, result.StatusCore);
            Assert.NotNull(result.Data);
            Assert.Equal("Successful", result.Message);
        }

        [Fact]
        public async Task GetSemesterById_NonExistingId_ReturnsNotFoundResponse()
        {
            var context = GetDbContext();
            var service = new SemestersRepository(context);
            var result = await service.GetSemesterByIdAsync(999);
            Assert.Equal(404, result.StatusCore);
            Assert.Null(result.Data);
            Assert.Equal("Semester not found", result.Message);
        }

        [Fact]
        public async Task CreateSemester_ValidData_ReturnsSuccessResponse()
        {
            var context = GetDbContext();
            context.AcademicSessions.Add(new AcademicSession { SessionId = 1, SessionName = "2023/2024" });
            context.SaveChanges();
            var service = new SemestersRepository(context);
            var newSemester = new SemestersDto
            {
                SemesterId = 3,
                SemesterName = "Summer 2024",
                SessionId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3)
            };
            var result = await service.CreateSemesterAsync(newSemester);
            Assert.Equal(200, result.StatusCore);
            Assert.NotNull(result.Data);
            Assert.Equal("Semester created successfully", result.Message);
        }

        [Fact]
        public async Task CreateSemester_DuplicateSemester_ReturnsBadRequestResponse()
        {
            var context = GetDbContext();
            context.AcademicSessions.Add(new AcademicSession { SessionId = 1, SessionName = "2023/2024" });
            context.Semesters.Add(new Semester { SemesterId = 1, SemesterName = "Fall 2023", SessionId = 1 });
            context.SaveChanges();
            var service = new SemestersRepository(context);
            var duplicateSemester = new SemestersDto
            {
                SemesterId = 1,
                SemesterName = "Fall 2023",
                SessionId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3)
            };
            var result = await service.CreateSemesterAsync(duplicateSemester);
            Assert.Equal(400, result.StatusCore);
            Assert.Null(result.Data);
            Assert.Equal("Semester already exists", result.Message);
        }

        [Fact]
        public async Task CreateSemester_NonExistingSession_ReturnsNotFoundResponse()
        {
            var context = GetDbContext();
            var service = new SemestersRepository(context);
            var newSemester = new SemestersDto
            {
                SemesterId = 3,
                SemesterName = "Summer 2024",
                SessionId = 999, // Non-existing session
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3)
            };
            var result = await service.CreateSemesterAsync(newSemester);
            Assert.Equal(404, result.StatusCore);
            Assert.Null(result.Data);
            Assert.Equal("Session does not exist", result.Message);
        }

        [Fact]
        public async Task DeleteSemester_ExistingId_ReturnsSuccessResponse()
        {
            var context = GetDbContext();
            var service = new SemestersRepository(context);
            var result = await service.DeleteSemesterAsync(1);
            Assert.Equal(200, result.StatusCore);
            Assert.NotNull(result.Data);
            Assert.Equal("Semester deleted successfully", result.Message);

        }

        [Fact]
        public async Task DeleteSemester_NonExistingId_ReturnsNotFoundResponse()
        {
            var context = GetDbContext();
            var service = new SemestersRepository(context);
            var result = await service.DeleteSemesterAsync(999);
            Assert.Equal(404, result.StatusCore);
            Assert.Null(result.Data);
            Assert.Equal("Semester not found", result.Message);
        }

        [Fact]
        public async Task UpdateSemester_ExistingId_ReturnsSuccessResponse()
        {
            var context = GetDbContext();
            var service = new SemestersRepository(context);
            var updateDto = new SemestersDto
            {
                SemesterName = "Updated Fall 2023",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4)
            };
            var result = await service.UpdateSemesterAsync(1, updateDto);
            Assert.Equal(200, result.StatusCore);
            Assert.NotNull(result.Data);
            Assert.Equal("Semester updated successfully", result.Message);
        }

        [Fact]
        public async Task UpdateSemester_NonExistingId_ReturnsNotFoundResponse()
        {
            var context = GetDbContext();
            var service = new SemestersRepository(context);
            var updateDto = new SemestersDto
            {
                SemesterName = "Updated Fall 2023",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4)
            };
            var result = await service.UpdateSemesterAsync(999, updateDto);
            Assert.Equal(404, result.StatusCore);
            Assert.Null(result.Data);
            Assert.Equal("Semester not found", result.Message);
        }

        [Fact]
        public async Task UpdateSemester_DuplicateSemester_ReturnsBadRequestResponse()
        {
            var context = GetDbContext();
            context.Semesters.Add(new Semester { SemesterId = 3, SemesterName = "Spring 2024", SessionId = 1 });
            context.SaveChanges();
            var service = new SemestersRepository(context);
            var updateDto = new SemestersDto
            {
                SemesterName = "Spring 2024", // Duplicate name
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4)
            };
            var result = await service.UpdateSemesterAsync(1, updateDto);
            Assert.Equal(400, result.StatusCore);
            Assert.Null(result.Data);
            Assert.Equal("Another semester with the same name exists", result.Message);
        }

        [Fact]
        public async Task UpdateSemester_NonExistingSession_ReturnsNotFoundResponse()
        {
            var context = GetDbContext();
            var service = new SemestersRepository(context);
            var updateDto = new SemestersDto
            {
                SemesterName = "Updated Fall 2023",
                SessionId = 999, // Non-existing session
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4)
            };
            var result = await service.UpdateSemesterAsync(1, updateDto);
            Assert.Equal(404, result.StatusCore);
            Assert.Null(result.Data);
            Assert.Equal("Session does not exist", result.Message);

        }

        [Fact]
        public async Task GetAllSemesters_EmptyDatabase_ReturnsEmptyList()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            var service = new SemestersRepository(context);
            var result = await service.GetAllSemestersAsync();
            Assert.Equal(200, result.StatusCore);
            Assert.NotNull(result.Data);
            var semesters = result.Data as List<Semester>;
            Assert.Empty(semesters);
            Assert.Equal("Successful", result.Message);
        }
    }
}
