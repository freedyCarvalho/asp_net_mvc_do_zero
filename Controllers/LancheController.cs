using LanchesMacCurso.Models;
using LanchesMacCurso.Repositories;
using LanchesMacCurso.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LanchesMacCurso.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult List()
        {
            ViewBag.Lanche = "Lanches";
            ViewData["Categoria"] = "Categoria";

            //var lanches = _lancheRepository.Lanches;
            //return View(lanches);

            var lanchesListViewModel = new LancheListViewModels();
            lanchesListViewModel.Lanches = _lancheRepository.Lanches;
            lanchesListViewModel.CategoriaAtual = "Categoria Atual";
            return View(lanchesListViewModel);
            
        }
    }
}