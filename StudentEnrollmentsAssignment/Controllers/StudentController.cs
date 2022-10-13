using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentsAssignment.Data;
using StudentEnrollmentsAssignment.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentsAssignment.ViewModels;

namespace StudentEnrollmentsAssignment.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentEnrollmentDbContext _context;

        public StudentController(StudentEnrollmentDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .Include(x => x.Enrollments)
                .ThenInclude(x => x.Class).ToListAsync();

            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View(new CreateStudentViewModelWithClasses(_context));
        }

        [HttpPost]
        public IActionResult Create(CreateStudentViewModel viewModel)
        {

            if (!ModelState.IsValid)
                return View(new CreateStudentViewModelWithClasses(_context));
            else
            {
                var student = new Student
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                };

                //Linq
                student.Enrollments = viewModel.ClassId
                    .Select(x => new Enrollment { ClassId = x, StudentId = student.Id })
                    .ToList();
                
                _context.Students.Add(student);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

        }

    }
}