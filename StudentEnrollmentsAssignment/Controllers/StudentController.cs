using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentsAssignment.Models;
using System.Diagnostics;

namespace StudentEnrollmentsAssignment.Controllers
{
    public class StudentController : Controller
    {

        public StudentController()
        {

        }

        public IActionResult Index()
        {

            return View();
        }

    }
}