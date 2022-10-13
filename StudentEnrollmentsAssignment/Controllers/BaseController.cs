using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentEnrollmentsAssignment.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
       
    }
}
