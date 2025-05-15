using LavrentevKT3122lb1.Database;
using LavrentevKT3122lb1.Interfaces;
using LavrentevKT3122lb1.Interfaces.LavrentevKT3122lb1.Interfaces;
using LavrentevKT3122lb1.Models;
using Microsoft.EntityFrameworkCore;

namespace LavrentevKT3122lb1.Tests
{
    public class TeachersServiceTests
    {
        private readonly TeacherDbContext _context;
        private readonly TeachersService _service;

        public TeachersServiceTests()
        {
            var options = new DbContextOptionsBuilder<TeacherDbContext>()
                .UseInMemoryDatabase(databaseName: "TeachersTestDb")
                .Options;

            _context = new TeacherDbContext(options);
            _service = new TeachersService(_context);

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
                FirstName = "Тест",
                LastName = "Тест",
                MiddleName = "Тест",
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
        public void GetTeachers_DefaultCall_ReturnsNonEmptyList()
        {
            // Act
            var result = _service.GetTeachers();

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetTeacherById_ExistingId_ReturnsTeacher()
        {
            // Arrange
            var existingId = 1;

            // Act
            var result = _service.GetTeacherById(existingId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingId, result.Id);
        }

        [Fact]
        public void GetTeacherById_NonExistingId_ReturnsNull()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _service.GetTeacherById(nonExistingId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateTeacher_ValidTeacher_AddsToDatabase()
        {
            // Arrange
            var initialCount = _context.Teachers.Count();
            var newTeacher = new Teacher
            {
                FirstName = "Test",
                LastName = "Test",
                MiddleName = "Test",
                BirthDate = new DateTime(1985, 5, 15),
                HireDate = DateTime.Now,
                DepartmentId = 1,
                PositionId = 1,
                IsDeleted = false
            };

            // Act
            _service.CreateTeacher(newTeacher);

            // Assert
            Assert.Equal(initialCount + 1, _context.Teachers.Count());
            Assert.Contains(_context.Teachers, t => t.FirstName == "Test");
        }

        [Fact]
        public void UpdateTeacher_ExistingTeacher_UpdatesProperties()
        {
            // Arrange
            var teacher = _context.Teachers.First();
            var newLastName = "UpdatedTest";
            teacher.LastName = newLastName;

            // Act
            _service.UpdateTeacher(teacher);

            // Assert
            var updatedTeacher = _context.Teachers.Find(teacher.Id);
            Assert.Equal(newLastName, updatedTeacher.LastName);
        }

        [Fact]
        public void DeleteTeacher_ExistingId_MarksAsDeleted()
        {
            // Arrange
            var teacherId = 1;

            // Act
            _service.DeleteTeacher(teacherId);

            // Assert
            var teacher = _context.Teachers.Find(teacherId);
            Assert.True(teacher.IsDeleted);
        }
    }
}