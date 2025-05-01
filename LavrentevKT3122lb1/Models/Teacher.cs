namespace LavrentevKT3122lb1.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
        public int? AcademicDegreeId { get; set; }

        public Department Department { get; set; }
        public Position Position { get; set; }
        public AcademicDegree AcademicDegree { get; set; }
        public ICollection<TeacherWorkload> Workloads { get; set; }
    }
}
