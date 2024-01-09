using Microsoft.AspNetCore.Mvc;
namespace LibraryManagementSystem.Controllers{
    public class ErrorController : Controller{
        public IActionResult Index(){
            return View();
        }
    }
}