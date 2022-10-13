namespace StudentEnrollmentsAssignment.Models
{
    public class Enrollment
    {

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int ClassId { get; set; }

        public Class Class { get; set; }
    }
}
