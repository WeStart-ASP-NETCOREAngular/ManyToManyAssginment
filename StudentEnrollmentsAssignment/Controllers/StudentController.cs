using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentsAssignment.Data;
using StudentEnrollmentsAssignment.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentsAssignment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace StudentEnrollmentsAssignment.Controllers
{
    public class StudentController : BaseController
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

        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "wasim" && password == "123")
            {

                var claims = new List<Claim>();
                claims.Add(new Claim("username", username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));

                // claimIdentity
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                // claimPrincipal 
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                await HttpContext.SignInAsync(claimPrincipal);

                return RedirectToAction(nameof(Create));

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {

            return View(new CreateStudentViewModelWithClasses(_context));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
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