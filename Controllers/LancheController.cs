using LanchesMacCurso.Models;
using LanchesMacCurso.Repositories;
using LanchesMacCurso.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IActionResult List(string categoria)
        {
            //ViewBag.Lanche = "Lanches";
            //ViewData["Categoria"] = "Categoria";

            //var lanches = _lancheRepository.Lanches;
            //return View(lanches);

            string _categoria = categoria;
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoria = "Todos os Lanches";
            }
            else
            {
                if (string.Equals("Normal", _categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lancheRepository.Lanches.Where(l => l.Categoria.CategoriaNome.Equals("Normal")).OrderBy(l => l.Nome);
                } else
                {
                    lanches = _lancheRepository.Lanches.Where(l => l.Categoria.CategoriaNome.Equals("Natural")).OrderBy(l => l.Nome);
                }

                /*
                // Fiz isso para poder otimizar o código, já que, acima, estamos repetindo a programação 
                // alterando, simplesmente o valor da string
                if (string.Equals("Normal", _categoria, StringComparison.OrdinalIgnoreCase))
                {
                    _categoria = "Normal";
                } else
                {
                    _categoria = "Natural";
                }

                lanches = _lancheRepository.Lanches.Where(l => l.Categoria.CategoriaNome.Equals(_categoria)).OrderBy(l => l.Nome);
                */

                categoriaAtual = _categoria;
            }

            /*
            var lanchesListViewModel = new LancheListViewModels();
            lanchesListViewModel.Lanches = _lancheRepository.Lanches;
            lanchesListViewModel.CategoriaAtual = "Categoria Atual";
            return View(lanchesListViewModel);
            */

            var lanchesListViewModel = new LancheListViewModels
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
            
        }
    
        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);

            if (lanche == null)
            {
                return View("~/Views/Error/Error.cshtml");
            } 

            return View(lanche);
        }
    
    }
}