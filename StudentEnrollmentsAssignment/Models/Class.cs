namespace StudentEnrollmentsAssignment.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<Enrollment> Enrollments { get; set; }
    }
}
