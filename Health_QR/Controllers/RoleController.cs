using Health_QR.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Health_QR.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = $"{Constants.Roles.Doctor}")]
        public IActionResult Doctor()
        {
            return View();
        }

        //[Authorize(Policy = "RequireAdmin")]
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}