using Microsoft.AspNetCore.Mvc;

namespace RMRBDClient.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
