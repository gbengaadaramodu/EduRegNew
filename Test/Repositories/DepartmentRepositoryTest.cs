
using EduReg.Common;
using EduReg.Controllers;
using EduReg.Data;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EduReg.Tests.Repositories
{
    public class DepartmentsRepositoryTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique DB for isolation
                .Options;

            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        private PagingParameters DefaultPaging =>
        new PagingParameters
        {
        PageNumber = 1,
        PageSize = 10
        };

        [Fact]
        public async Task CreateDepartmentAsync_ShouldReturnSuccess()
        {
            var context = GetDbContext();
            var repo = new DepartmentsRepository(context);
            var dto = new DepartmentsDto
            {
                DepartmentCode = "BIO001",
                DepartmentName = "Biology",
                Description = "Bio Dept",
                Duration = 4,
                NumberofSemesters = 8,
                MaximumNumberofSemesters = 10,
                FacultyCode = "SCI",
                Programme = "BSc"
            };

            var result = await repo.CreateDepartmentAsync(dto);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetDepartmentByIdAsync_ShouldReturnDepartment_WhenExists()
        {
            var context = GetDbContext();
            var department = new Departments
            {
                DepartmentCode = "CSE01",
                DepartmentName = "CS",
                Description = "CS Dept",
                Duration = 4,
                FacultyCode = "SCI",
               //  Programme = "BSc",
                NumberofSemesters = 8,
                MaximumNumberofSemesters = 10
            };
            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();

            var repo = new DepartmentsRepository(context);
            var result = await repo.GetDepartmentByIdAsync(department.Id);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetDepartmentByNameAsync_ShouldReturnDepartment_WhenNameExists()
        {
            var context = GetDbContext();
            await context.Departments.AddAsync(new Departments
            {
                DepartmentCode = "CSE02",
                DepartmentName = "Software",
                Description = "Soft Dept",
                FacultyCode = "SCI",
                Duration = 4,
               // Programme = "BSc",
                NumberofSemesters = 8,
                MaximumNumberofSemesters = 10
            });
            await context.SaveChangesAsync();

            var repo = new DepartmentsRepository(context);
            var result = await repo.GetDepartmentByNameAsync("software");

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllDepartmentsAsync_ShouldReturnList()
        {
            var context = GetDbContext();
            await context.Departments.AddAsync(new Departments
            {
                DepartmentCode = "ENG01",
                DepartmentName = "English",
                FacultyCode = "ART",
                Duration = 4,
                NumberofSemesters = 8,
                MaximumNumberofSemesters = 10
            });
            await context.SaveChangesAsync();

            var repo = new DepartmentsRepository(context);

            // Act
            var result = await repo.GetAllDepartmentsAsync(DefaultPaging);

            // Assert
            result.StatusCode.Should().Be(200);

            var data = result.Data as IEnumerable<Departments>;
            data.Should().NotBeNull();
            data!.Count().Should().BeGreaterThan(0);
        }


        [Fact]
        public async Task UpdateDepartmentAsync_ShouldUpdateDepartment()
        {
            var context = GetDbContext();
            var dept = new Departments
            {
                DepartmentCode = "MATH01",
                DepartmentName = "Mathematics",
                Description = "Math Dept",
                FacultyCode = "SCI",
               // Programme = "BSc",
                Duration = 4,
                NumberofSemesters = 8,
                MaximumNumberofSemesters = 10
            };
            await context.Departments.AddAsync(dept);
            await context.SaveChangesAsync();

            var repo = new DepartmentsRepository(context);
            var updateDto = new DepartmentsDto
            {
                DepartmentCode = dept.DepartmentCode,
                DepartmentName = "Pure Math",
                Description = "Updated Desc",
                FacultyCode = dept.FacultyCode,
              //  Programme = dept.Programme,
                Duration = 4,
                NumberofSemesters = 8,
                MaximumNumberofSemesters = 10
            };

            var result = await repo.UpdateDepartmentAsync(dept.Id, updateDto);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteDepartmentAsync_ShouldDeleteSuccessfully()
        {
            var context = GetDbContext();
            var dept = new Departments
            {
                DepartmentCode = "HIS01",
                DepartmentName = "History",
                FacultyCode = "ART",
                Duration = 4,
              //  Programme = "BA",
                NumberofSemesters = 8,
                MaximumNumberofSemesters = 10
            };
            await context.Departments.AddAsync(dept);
            await context.SaveChangesAsync();

            var repo = new DepartmentsRepository(context);
            var result = await repo.DeleteDepartmentAsync(dept.Id);

            result.StatusCode.Should().Be(200);
        }
    }
}









