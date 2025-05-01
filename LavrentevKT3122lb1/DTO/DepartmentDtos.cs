namespace LavrentevKT3122lb1.DTO
{
    public class DepartmentShortDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FoundationDate { get; set; }
        public int TeachersCount { get; set; }
        public TeacherShortDto Head { get; set; }
    }

    public class DepartmentDetailsDto : DepartmentShortDto
    {
        public List<TeacherShortDto> Teachers { get; set; } = new();
      
    }

}
