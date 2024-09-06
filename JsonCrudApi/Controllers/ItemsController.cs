using Microsoft.AspNetCore.Mvc;

namespace JsonCrudApi.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
