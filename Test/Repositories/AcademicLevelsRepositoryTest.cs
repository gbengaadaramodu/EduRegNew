using AutoFixture;
using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EduReg.Tests
{
    public class AcademicLevelsRepositoryTest
    {
        private readonly AcademicLevelsRepository _repository;
        private readonly ApplicationDbContext _context;
        private readonly Fixture _fixture;

        public AcademicLevelsRepositoryTest()
        {
            _fixture = new Fixture();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid()) 
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new AcademicLevelsRepository(_context);
        }

        [Fact]
        public async Task CreateAcademicLevel_ShouldReturnCreatedResponse()
        {
            var dto = _fixture.Create<AcademicLevelsDto>();

            var result = await _repository.CreateAcademicLevel(dto);

            result.StatusCore.Should().Be(201);
            result.Data.Should().BeOfType<AcademicLevel>();

            var saved = await _context.AcademicLevels.FirstOrDefaultAsync(a => a.LevelName == dto.LevelName);
            saved.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllAcademicLevelAsync_ShouldReturnLevels()
        {
            var levels = _fixture.CreateMany<AcademicLevel>(3).ToList();
            _context.AcademicLevels.AddRange(levels);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAcademicLevelAsync();

            result.StatusCore.Should().Be(200);
            ((IEnumerable<AcademicLevel>)result.Data!).Count().Should().Be(3);
        }

        [Fact]
        public async Task GetAcademicLevelByIdAsync_ShouldReturnLevel_WhenFound()
        {
            var level = _fixture.Create<AcademicLevel>();
            _context.AcademicLevels.Add(level);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAcademicLevelByIdAsync(level.Id);

            result.StatusCore.Should().Be(200);
            result.Data.Should().Be(level);
        }

        [Fact]
        public async Task UpdateAcademicLevelAsync_ShouldUpdateAndReturnLevel()
        {
            var level = _fixture.Create<AcademicLevel>();
            _context.AcademicLevels.Add(level);
            await _context.SaveChangesAsync();

            var dto = _fixture.Create<AcademicLevelsDto>();

            var result = await _repository.UpdateAcademicLevelAsync(level.Id, dto);

            result.StatusCore.Should().Be(200);
            result.Data.Should().Be(level);
            level.LevelName.Should().Be(dto.LevelName);
            level.Description.Should().Be(dto.Description);
        }

        [Fact]
        public async Task DeleteAcademicLevelAsync_ShouldRemoveLevel()
        {
            var level = _fixture.Create<AcademicLevel>();
            _context.AcademicLevels.Add(level);
            await _context.SaveChangesAsync();

            var result = await _repository.DeleteAcademicLevelAsync(level.Id);

            result.StatusCore.Should().Be(200);

            var deleted = await _context.AcademicLevels.FindAsync(level.Id);
            deleted.Should().BeNull();
        }
    }
}
