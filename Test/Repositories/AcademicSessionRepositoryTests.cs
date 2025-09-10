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
    public class AcademicSessionRepositoryTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);

            context.AcademicSessions.AddRange(
                new AcademicSession { SessionId = 1, SessionName = "2023/2024" }
            );
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetAllSessions_ReturnsSuccessResponse()
        {
            var context = GetDbContext();
            var service = new AcademicSessionsRepository(context);

            var result = await service.GetAllAcademicSessionsAsync();

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCore);
            Assert.Equal("Successful", result.Message);
            Assert.IsType<List<AcademicSession>>(result.Data);
        }

        [Fact]
        public async Task GetSessionById_ExistingId_ReturnsSuccessResponse()
        {
            var context = GetDbContext();
            var service = new AcademicSessionsRepository(context);

            var result = await service.GetAcademicSessionByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCore);
            Assert.Equal("Successful", result.Message);
            Assert.IsType<AcademicSession>(result.Data);
        }

        [Fact]
        public async Task GetSessionById_NonExistingId_ReturnsNotFoundResponse()
        {
            var context = GetDbContext();
            var service = new AcademicSessionsRepository(context);
            var result = await service.GetAcademicSessionByIdAsync(99);
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCore);
            Assert.Equal("Session not found", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task CreateSession_ValidModel_ReturnsSuccessResponse()
        {
            var context = GetDbContext();
            var service = new AcademicSessionsRepository(context);
            var newSession = new AcademicSessionsDto { SessionName = "2024/2025" };
            var result = await service.CreateAcademicSessionAsync(newSession);
            Assert.Equal("Session created successfully", result.Message);
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCore);
            
            Assert.IsType<AcademicSession>(result.Data);
        }

        [Fact]
        public async Task CreateSession_DuplicateSession_ReturnsBadRequestResponse()
        {
            var context = GetDbContext();
            var service = new AcademicSessionsRepository(context);
            var duplicateSession = new AcademicSessionsDto { SessionName = "2023/2024" };
            var result = await service.CreateAcademicSessionAsync(duplicateSession);
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCore);
            Assert.Equal("Session already exists", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task DeleteSession_ExistingId_ReturnsSuccessResponse()
        {
            var context = GetDbContext();
            var service = new AcademicSessionsRepository(context);
            var result = await service.DeleteAcademicSessionAsync(1);
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCore);
            Assert.Equal("Session deleted successfully", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task DeleteSession_NonExistingId_ReturnsNotFoundResponse()
        {
            var context = GetDbContext();
            var service = new AcademicSessionsRepository(context);
            var result = await service.DeleteAcademicSessionAsync(99);
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCore);
            Assert.Equal("Session not found", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task UpdateSession_ExistingId_ReturnsSucessResponse()
        {
            var context = GetDbContext();
            var service = new AcademicSessionsRepository(context);
            var updateSession = new AcademicSessionsDto { SessionId = 1, SessionName = "2024/2025" };
            var result = await service.UpdateAcademicSessionAsync(1, updateSession);
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCore);
            Assert.Equal("Session updated successfully", result.Message);
        }

        [Fact]
        public async Task UpdateSession_NonExistingId_ReturnsNotFoundResponse()
        {
            var context = GetDbContext();
            var service = new AcademicSessionsRepository(context);
            var updateSession = new AcademicSessionsDto { SessionId = 99, SessionName = "2024/2025" };
            var result = await service.UpdateAcademicSessionAsync(99, updateSession);
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCore);
            Assert.Equal("Session does not exist", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task UpdateSession_DuplicateSessionName_ReturnsBadRequestResponse()
        {
            var context = GetDbContext();
            context.AcademicSessions.Add(new AcademicSession { SessionId = 2, SessionName = "2024/2025" });
            context.SaveChanges();
            var service = new AcademicSessionsRepository(context);
            var updateSession = new AcademicSessionsDto { SessionId = 1, SessionName = "2024/2025" };
            var result = await service.UpdateAcademicSessionAsync(1, updateSession);
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCore);
            Assert.Equal("Another session with the same name already exists", result.Message);
            Assert.Null(result.Data);
        }
    }
}
