using LavrentevKT3122lb1.Database;
using LavrentevKT3122lb1.DTO;
using LavrentevKT3122lb1.Interfaces.LavrentevKT3122lb1.Interfaces;
using LavrentevKT3122lb1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LavrentevKT3122lb1.Controllers
{


    namespace LavrentevKT3122lb1.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class TeachersController : ControllerBase
        {
            private readonly TeacherDbContext _context;

            public TeachersController(TeacherDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<TeacherShortDto>>> GetTeachers()
            {
                return await _context.Teachers
                    .Where(t => !t.IsDeleted)
                    .Select(t => new TeacherShortDto
                    {
                        Id = t.Id,
                        FullName = $"{t.LastName} {t.FirstName} {t.MiddleName}"
                    })
                    .ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<TeacherDetailsDto>> GetTeacher(int id)
            {
                var teacher = await _context.Teachers
                    .Where(t => t.Id == id && !t.IsDeleted)
                    .Select(t => new TeacherDetailsDto
                    {
                        Id = t.Id,
                        FullName = $"{t.LastName} {t.FirstName} {t.MiddleName}",
                        BirthDate = (DateTime)t.BirthDate,
                        HireDate = t.HireDate,
                        Department = new DepartmentShortDto
                        {
                            Id = t.Department.Id,
                            Name = t.Department.Name,
                            FoundationDate = t.Department.FoundationDate
                        },
                        Position = new PositionDto
                        {
                            Id = t.Position.Id,
                            Name = t.Position.Name
                        },
                        AcademicDegree = t.AcademicDegree != null ? new AcademicDegreeDto
                        {
                            Id = t.AcademicDegree.Id,
                            Name = t.AcademicDegree.Name
                        } : null,
                        Workloads = t.Workloads
                            .Where(w => !w.IsDeleted)
                            .Select(w => new WorkloadDto
                            {
                                Hours = w.Hours,
                                Semester = w.Semester,
                                Year = w.Year,
                                Discipline = new DisciplineDto
                                {
                                    Id = w.Discipline.Id,
                                    Name = w.Discipline.Name,
                                    Code = w.Discipline.Code
                                }
                            })
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (teacher == null)
                {
                    return NotFound();
                }

                return teacher;
            }

            [HttpPost]
            public async Task<ActionResult<Teacher>> CreateTeacher(TeacherCreateDto teacherDto)
            {
                var teacher = new Teacher
                {
                    FirstName = teacherDto.FirstName,
                    LastName = teacherDto.LastName,
                    MiddleName = teacherDto.MiddleName,
                    BirthDate = teacherDto.BirthDate,
                    HireDate = teacherDto.HireDate,
                    DepartmentId = teacherDto.DepartmentId,
                    PositionId = teacherDto.PositionId,
                    AcademicDegreeId = teacherDto.AcademicDegreeId,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTeacher), new { id = teacher.Id }, teacherDto);
            }



            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteTeacher(int id)
            {
                var teacher = await _context.Teachers.FindAsync(id);
                if (teacher == null || teacher.IsDeleted)
                {
                    return NotFound();
                }

                teacher.IsDeleted = true;
                teacher.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool TeacherExists(int id)
            {
                return _context.Teachers.Any(e => e.Id == id && !e.IsDeleted);
            }
        }
    }
}