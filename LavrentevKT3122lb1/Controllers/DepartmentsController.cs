using LavrentevKT3122lb1.Database;
using LavrentevKT3122lb1.DTO;
using LavrentevKT3122lb1.Extensions;
using LavrentevKT3122lb1.Interfaces;
using LavrentevKT3122lb1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LavrentevKT3122lb1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly TeacherDbContext _context;

        public DepartmentsController(TeacherDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentShortDto>>> GetDepartments(
       [FromQuery] DateTime? foundationDateFrom,
       [FromQuery] DateTime? foundationDateTo,
       [FromQuery] int? minTeachersCount,
       [FromQuery] int? maxTeachersCount)
        {
            var query = _context.Departments
                .Where(d => !d.IsDeleted)
                .Include(d => d.Head)
                .Include(d => d.Teachers.Where(t => !t.IsDeleted))
                .AsQueryable();

            // Фильтрация по дате основания
            if (foundationDateFrom.HasValue)
            {
                query = query.Where(d => d.FoundationDate >= foundationDateFrom.Value);
            }

            if (foundationDateTo.HasValue)
            {
                query = query.Where(d => d.FoundationDate <= foundationDateTo.Value);
            }

            // Фильтрация по количеству преподавателей
            if (minTeachersCount.HasValue)
            {
                query = query.Where(d => d.Teachers.Count >= minTeachersCount.Value);
            }

            if (maxTeachersCount.HasValue)
            {
                query = query.Where(d => d.Teachers.Count <= maxTeachersCount.Value);
            }

            var result = await query
                .Select(d => new DepartmentShortDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    FoundationDate = d.FoundationDate,
                    Head = d.Head != null ? new TeacherShortDto
                    {
                        Id = d.Head.Id,
                        FullName = $"{d.Head.LastName} {d.Head.FirstName}"
                    } : null,
                    TeachersCount = d.Teachers.Count 
                })
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var department = _context.Departments
                .Where(d => d.Id == id && !d.IsDeleted)
                .Select(d => new DepartmentDetailsDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    FoundationDate = d.FoundationDate,
                    Head = d.Head != null ? new TeacherShortDto
                    {
                        Id = d.Head.Id,
                        FullName = $"{d.Head.LastName} {d.Head.FirstName}"
                    } : null,
                    Teachers = d.Teachers
                        .Where(t => !t.IsDeleted)
                        .Select(t => new TeacherShortDto
                        {
                            Id = t.Id,
                            FullName = $"{t.LastName} {t.FirstName}"
                        })
                        .ToList()
                })
                .FirstOrDefault();

            if (department == null)
                return NotFound();

            return Ok(department);
        }

            [HttpGet("error")]
            public IActionResult TriggerError()
            {
                throw new Exception("Это тестовое исключение для проверки middleware");
            }
    }
}