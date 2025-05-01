using LavrentevKT3122lb1.Database;
using LavrentevKT3122lb1.Models;
using Microsoft.EntityFrameworkCore;
namespace LavrentevKT3122lb1.Interfaces
{
    public interface IDepartmentsService
    {
        IEnumerable<Department> GetDepartments(DepartmentFilter? filter = null);
        Department? GetDepartmentById(int id);
    }

    public class DepartmentsService : IDepartmentsService
    {
        private readonly TeacherDbContext _context;

        public DepartmentsService(TeacherDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetDepartments(DepartmentFilter? filter = null)
        {
            var query = _context.Departments
                .Where(d => !d.IsDeleted)
                .Include(d => d.Head)
                .Include(d => d.Teachers.Where(t => !t.IsDeleted))
                .AsQueryable();

            if (filter != null)
            {
                if (filter.FoundationDateFrom.HasValue)
                    query = query.Where(d => d.FoundationDate >= filter.FoundationDateFrom.Value);

                if (filter.FoundationDateTo.HasValue)
                    query = query.Where(d => d.FoundationDate <= filter.FoundationDateTo.Value);

                if (filter.MinTeachersCount.HasValue)
                    query = query.Where(d => d.Teachers.Count >= filter.MinTeachersCount.Value);

                if (filter.MaxTeachersCount.HasValue)
                    query = query.Where(d => d.Teachers.Count <= filter.MaxTeachersCount.Value);
            }

            return query.ToList();
        }

        public Department? GetDepartmentById(int id)
        {
            return _context.Departments
                .Include(d => d.Head)
                .Include(d => d.Teachers.Where(t => !t.IsDeleted))
                .FirstOrDefault(d => d.Id == id && !d.IsDeleted);
        }
    }

    public class DepartmentFilter
    {
        public DateTime? FoundationDateFrom { get; set; }
        public DateTime? FoundationDateTo { get; set; }
        public int? MinTeachersCount { get; set; }
        public int? MaxTeachersCount { get; set; }
    }
}

