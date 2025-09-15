using EduReg.Common;
using EduReg.Controllers;
using EduReg.Data;
using EduReg.Managers;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using EduReg.Services.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace EduReg.Tests.Controllers
{
    
    
        public class SchoolsControllerTests
        {
            private readonly Mock<IDepartments> _mockDept;
            private readonly Mock<IFaculties> _mockFaculties;
            private readonly Mock<IProgrammes> _mockProgrammes;
            private readonly Mock<ILogger<SchoolsController>> _mockLogger;
            private readonly SchoolsController _controller;

            public SchoolsControllerTests()
            {
                // Create mocks
                _mockDept = new Mock<IDepartments>();
                _mockFaculties = new Mock<IFaculties>();
                _mockProgrammes = new Mock<IProgrammes>();
                _mockLogger = new Mock<ILogger<SchoolsController>>();

                // Create real SchoolManager instance with mocked dependencies
                var schoolManager = new SchoolsManager(_mockFaculties.Object, _mockDept.Object, _mockProgrammes.Object);

                // Create controller with real SchoolManager instance
               _controller = new SchoolsController(_mockLogger.Object, schoolManager);
            }

            [Fact]
            public async Task CreateDepartmentAsync_ShouldReturn200()
            {
                var dto = new DepartmentsDto { DepartmentName = "New Dept", DepartmentCode = "ND01" };
                _mockDept.Setup(x => x.CreateDepartmentAsync(dto))
                         .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

                var result = await _controller.CreateDepartmentAsync(dto);
                var response = result as ObjectResult;
                Assert.NotNull(response);
                Assert.Equal(200, response.StatusCode);
            }

            [Fact]
            public async Task CreateFacultyAsync_ShouldReturn200()
            {
                var dto = new FacultiesDto { FacultyName = "New Faculty", FacultyCode = "NF01" };
                _mockFaculties.Setup(x => x.CreateFacultyAsync(dto))
                              .ReturnsAsync(new GeneralResponse { StatusCore = 200 });
                var result = await _controller.CreateFacultyAsync(dto);
                var response = result as ObjectResult;
                Assert.NotNull(response);
                Assert.Equal(200, response.StatusCode);                            
            }

            [Fact]
            public async Task GetDepartmentByIdAsync_ShouldReturn200()
            {
                _mockDept.Setup(x => x.GetDepartmentByIdAsync(1))
                         .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

                var result = await _controller.GetDepartmentByIdAsync(1);
                var response = result as ObjectResult;
                Assert.NotNull(response);
                Assert.Equal(200, response.StatusCode);
            }

            [Fact]
            public async Task GetFacultyByIdAsync_ShouldReturn200()
            {
                _mockFaculties.Setup(x => x.GetFacultyByIdAsync(1))
                         .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

                var result = await _controller.GetFacultyByIdAsync(1);
                var response = result as ObjectResult;
                Assert.NotNull(response);
                Assert.Equal(200, response.StatusCode);
            }


            [Fact]
            public async Task GetDepartmentByNameAsync_ShouldReturn200()
            {
                _mockDept.Setup(x => x.GetDepartmentByNameAsync("Physics"))
                         .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

                var result = await _controller.GetDepartmentByNameAsync("Physics");
                var response = result as ObjectResult;
                Assert.NotNull(response);
                Assert.Equal(200, response.StatusCode);
            }

            [Fact]
            public async Task GetAllDepartmentsAsync_ShouldReturn200()
            {
                _mockDept.Setup(x => x.GetAllDepartmentsAsync())
                         .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

                var result = await _controller.GetAllDepartmentsAsync();
                var response = result as ObjectResult;
                Assert.NotNull(response);
                Assert.Equal(200, response.StatusCode);
            }

            [Fact]
            public async Task GetAllFacultiesAsync_ShouldReturn200()
            {
                _mockFaculties.Setup(x => x.GetAllFacultiesAsync())
                         .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

                var result = await _controller.GetAllFacultiesAsync();
                var response = result as ObjectResult;
                Assert.NotNull(response);
                Assert.Equal(200, response.StatusCode);
            }

            [Fact]
            public async Task UpdateDepartmentAsync_ShouldReturn200()
            {
                var dto = new DepartmentsDto { DepartmentName = "Updated" };
                _mockDept.Setup(x => x.UpdateDepartmentAsync(1, dto))
                         .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

                var result = await _controller.UpdateDepartmentAsync(1, dto);
                var response = result as ObjectResult;
                Assert.NotNull(response);
                Assert.Equal(200, response.StatusCode);
            }

            [Fact]
            public async Task UpdateFacultyAsync_ShouldReturn200()
            {
                var dto = new FacultiesDto { FacultyName = "Updated" };
                _mockFaculties.Setup(x => x.UpdateFacultyAsync(1, dto))
                         .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

                var result = await _controller.UpdateFacultyAsync(1, dto);
                var response = result as ObjectResult;
                Assert.NotNull(response);
                Assert.Equal(200, response.StatusCode);
            }

            [Fact]
            public async Task DeleteDepartmentAsync_ShouldReturn200()
            {
                _mockDept.Setup(x => x.DeleteDepartmentAsync(1))
                         .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

                var result = await _controller.DeleteDepartmentAsync(1);
                var response = result as ObjectResult;
                Assert.NotNull(response);
                Assert.Equal(200, response.StatusCode);
            }

            [Fact]
            public async Task DeleteFacultyAsync_ShouldReturn200()
            {
                _mockFaculties.Setup(x => x.DeleteFacultyAsync(1))
                         .ReturnsAsync(new GeneralResponse { StatusCore = 200 });

                var result = await _controller.DeleteFacultyAsync(1);
                var response = result as ObjectResult;
                Assert.NotNull(response);
                Assert.Equal(200, response.StatusCode);
            }
    }
    
}

