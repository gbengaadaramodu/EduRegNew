// File: EduReg.Tests/FacultiesRepositoryTests.cs
using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

public class FacultiesRepositoryTests
{
    private static ApplicationDbContext BuildContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    private PagingParameters DefaultPaging =>
    new PagingParameters
    {
        PageNumber = 1,
        PageSize = 10
    };

    [Fact]
    public async Task UpdateFacultyAsync_Should_Return200_And_UpdateFields_When_FacultyExists()
    {
        // Arrange
        using var ctx = BuildContext();
        var repo = new FacultiesRepository(ctx);

        var existing = new Faculties
        {
            Id = 1,
            FacultyName = "Old Name",
            FacultyCode = "OLD",
            Description = "Old Desc",
            InstitutionShortName = "OLDUNI"
        };
        ctx.Faculties.Add(existing);
        await ctx.SaveChangesAsync();

        var dto = new FacultiesDto
        {
            FacultyName = "New Name",
            FacultyCode = "NEW",
            Description = "New Desc",
            InstitutionShortName = "NEWUNI"
        };

        // Act
        var result = await repo.UpdateFacultyAsync(1, dto);

        // Assert
        result.StatusCode.Should().Be(200);
        result.Message.Should().Be("Faculty updated successfully");

        var updated = await ctx.Faculties.FindAsync(1);
        updated!.FacultyName.Should().Be("New Name");
        updated.FacultyCode.Should().Be("NEW");
        updated.Description.Should().Be("New Desc");
        updated.InstitutionShortName.Should().Be("NEWUNI");
    }

    [Fact]
    public async Task UpdateFacultyAsync_Should_Return404_When_NotFound()
    {
        using var ctx = BuildContext();
        var repo = new FacultiesRepository(ctx);

        var dto = new FacultiesDto
        {
            FacultyName = "X",
            FacultyCode = "X",
            Description = "X",
            InstitutionShortName = "X"
        };

        var result = await repo.UpdateFacultyAsync(999, dto);

        result.StatusCode.Should().Be(404);
        result.Data.Should().BeNull();
    }

    [Fact]
    
    //public async Task GetAllFacultiesAsync_Should_Return200_With_Paged_List()
    //{
    //    using var ctx = BuildContext();
    //    var repo = new FacultiesRepository(ctx);

    //    ctx.Faculties.Add(new Faculties { FacultyName = "Sci", FacultyCode = "SCI" });
    //    ctx.Faculties.Add(new Faculties { FacultyName = "Eng", FacultyCode = "ENG" });
    //    await ctx.SaveChangesAsync();

    //    // Act
    //    var result = await repo.GetAllFacultiesAsync(DefaultPaging);

    //    // Assert
    //    result.StatusCode.Should().Be(200);
    //    result.Data.Should().NotBeNull();

    //    var data = result.Data as IEnumerable<Faculties>;
    //    data.Should().NotBeNull();
    //    data!.Count().Should().Be(2);
    //}


    //[Fact]
    public async Task DeleteFacultyAsync_Should_Return200_When_Deleted()
    {
        using var ctx = BuildContext();
        var repo = new FacultiesRepository(ctx);

        ctx.Faculties.Add(new Faculties { Id = 7, FacultyName = "ToDel", FacultyCode = "DEL" });
        await ctx.SaveChangesAsync();

        var result = await repo.DeleteFacultyAsync(7);

        result.StatusCode.Should().Be(200);
        (await ctx.Faculties.FindAsync(7)).Should().BeNull();
    }
}
