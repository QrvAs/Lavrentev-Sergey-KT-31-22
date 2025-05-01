using LavrentevKT3122lb1.Database;
using LavrentevKT3122lb1.Models;
using Microsoft.EntityFrameworkCore;
namespace LavrentevKT3122lb1.Interfaces
{
    namespace LavrentevKT3122lb1.Interfaces
    {
        public interface ITeachersService
        {
            IEnumerable<Teacher> GetTeachers();
            Teacher? GetTeacherById(int id);
            void CreateTeacher(Teacher teacher);
            void UpdateTeacher(Teacher teacher);
            void DeleteTeacher(int id);
        }

        public class TeachersService : ITeachersService
        {
            private readonly TeacherDbContext _context;

            public TeachersService(TeacherDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Teacher> GetTeachers()
            {
                return _context.Teachers
                    .Where(t => !t.IsDeleted)
                    .Include(t => t.Department)
                    .Include(t => t.Position)
                    .Include(t => t.AcademicDegree)
                    .ToList();
            }

            public Teacher? GetTeacherById(int id)
            {
                return _context.Teachers
                    .Include(t => t.Department)
                    .Include(t => t.Position)
                    .Include(t => t.AcademicDegree)
                    .FirstOrDefault(t => t.Id == id && !t.IsDeleted);
            }

            public void CreateTeacher(Teacher teacher)
            {
                teacher.CreatedAt = DateTime.UtcNow;
                teacher.UpdatedAt = DateTime.UtcNow;
                teacher.IsDeleted = false;

                _context.Teachers.Add(teacher);
                _context.SaveChanges();
            }

            public void UpdateTeacher(Teacher teacher)
            {
                teacher.UpdatedAt = DateTime.UtcNow;
                _context.Teachers.Update(teacher);
                _context.SaveChanges();
            }

            public void DeleteTeacher(int id)
            {
                var teacher = _context.Teachers.Find(id);
                if (teacher != null)
                {
                    teacher.IsDeleted = true;
                    teacher.UpdatedAt = DateTime.UtcNow;
                    _context.SaveChanges();
                }
            }
        }
    }
}