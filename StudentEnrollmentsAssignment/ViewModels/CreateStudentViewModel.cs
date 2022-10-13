using Microsoft.AspNetCore.Mvc.Rendering;
using StudentEnrollmentsAssignment.Data;
using StudentEnrollmentsAssignment.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentEnrollmentsAssignment.ViewModels
{
    public class CreateStudentViewModel
    {
        [Display(Name ="First Name")]
        [Required(ErrorMessage = "ادخل الاسم الاول")]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        //To Get From HttpPost
        [Display(Name = "Select Classes")]
        public List<int> ClassId { get; set; }

        //public List<SelectListItem>? Classes { get; set; }

    }

    public class CreateStudentViewModelWithClasses : CreateStudentViewModel
    {

        public CreateStudentViewModelWithClasses(StudentEnrollmentDbContext context)
        {
            var clesses = context.Classes
                .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() })
                .ToList();
            Classes = clesses;
        }

        public List<SelectListItem> Classes { get; set; }

    }
}
