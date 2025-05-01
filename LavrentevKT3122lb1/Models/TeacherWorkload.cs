namespace LavrentevKT3122lb1.Models
{
    public class TeacherWorkload
    {
        public int Id { get; set; }
        public int Hours { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int TeacherId { get; set; }
        public int DisciplineId { get; set; }

        public Teacher Teacher { get; set; }
        public Discipline Discipline { get; set; }
    }
}
