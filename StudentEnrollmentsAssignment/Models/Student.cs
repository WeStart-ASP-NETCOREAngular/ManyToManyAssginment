using System.ComponentModel.DataAnnotations.Schema;

namespace StudentEnrollmentsAssignment.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<Enrollment> Enrollments { get; set; }

        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

    }
}
