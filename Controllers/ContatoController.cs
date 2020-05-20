using Microsoft.AspNetCore.Mvc;

namespace LanchesMacCurso.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}