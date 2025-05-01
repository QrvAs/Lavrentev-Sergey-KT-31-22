namespace LavrentevKT3122lb1.DTO
{

    public class TeacherShortDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class TeacherDetailsDto : TeacherShortDto
    {
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public DepartmentShortDto Department { get; set; }
        public PositionDto Position { get; set; }
        public AcademicDegreeDto AcademicDegree { get; set; }
        public List<WorkloadDto> Workloads { get; set; } = new();
    }
}
