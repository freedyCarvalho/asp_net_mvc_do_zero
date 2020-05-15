
//using LanchesMacCurso.Repositories;
//using LanchesMacCurso.ViewModels;
//using Microsoft.AspNetCore.Mvc;

//namespace LanchesMacCurso.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILancheRepository _lancheRepository;

//        public HomeController(ILancheRepository lancheRepository)
//        {
//            _lancheRepository = lancheRepository;
//        }

//        public IActionResult Index()
//        {
//            var homeViewModel = new HomeViewModels
//            {
//                LanchesPreferidos = _lancheRepository.LanchesPreferidos
//            };
            
//            return View(homeViewModel);
//        }

//    }
//}
