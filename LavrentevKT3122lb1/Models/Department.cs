namespace LavrentevKT3122lb1.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FoundationDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        public int? HeadId { get; set; }
        public Teacher Head { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
  
}
