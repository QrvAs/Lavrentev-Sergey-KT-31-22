using LavrentevKT3122lb1.Controllers.LavrentevKT3122lb1.Controllers;
using LavrentevKT3122lb1.Database;
using LavrentevKT3122lb1.DTO;
using LavrentevKT3122lb1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LavrentevKT3122lb1.Tests
{
    public class TeachersControllerIntegrationTests
    {
        private readonly TeacherDbContext _context;
        private readonly TeachersController _controller;

        public TeachersControllerIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<TeacherDbContext>()
                .UseInMemoryDatabase(databaseName: "TeachersIntegrationTestDb")
                .Options;

            _context = new TeacherDbContext(options);
            _controller = new TeachersController(_context);

            InitializeTestData();
        }

        private void InitializeTestData()
        {
            var department = new Department { Name = "Test", FoundationDate = DateTime.Now };
            var position = new Position { Name = "Test" };
            var degree = new AcademicDegree { Name = "Test" };

            _context.Departments.Add(department);
            _context.Positions.Add(position);
            _context.AcademicDegrees.Add(degree);
            _context.SaveChanges();

            var teacher = new Teacher
            {
                FirstName = "Test",
                LastName = "Test",
                MiddleName = "Test",
                BirthDate = new DateTime(1980, 1, 1),
                HireDate = DateTime.Now,
                DepartmentId = department.Id,
                PositionId = position.Id,
                AcademicDegreeId = degree.Id,
                IsDeleted = false
            };

            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetTeachers_ReturnsListOfTeachers()
        {
            // Act
            var result = await _controller.GetTeachers();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TeacherShortDto>>>(result);
            var returnValue = Assert.IsType<List<TeacherShortDto>>(actionResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetTeacher_ExistingId_ReturnsTeacher()
        {
            // Arrange
            var existingId = 2;

            // Act
            var result = await _controller.GetTeacher(existingId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<TeacherDetailsDto>>(result);
            var teacher = Assert.IsType<TeacherDetailsDto>(actionResult.Value);
            Assert.Equal(existingId, teacher.Id);
        }

        [Fact]
        public async Task GetTeacher_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = await _controller.GetTeacher(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateTeacher_ValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            var newTeacher = new TeacherCreateDto
            {
                FirstName = "Test",
                LastName = "Test",
                MiddleName = "Test",
                BirthDate = new DateTime(1985, 5, 15),
                HireDate = DateTime.Now,
                DepartmentId = 1,
                PositionId = 1
            };

            // Act
            var result = await _controller.CreateTeacher(newTeacher);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(TeachersController.GetTeacher), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteTeacher_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var existingId = 1;

            // Act
            var result = await _controller.DeleteTeacher(existingId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTeacher_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = await _controller.DeleteTeacher(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


    }
}