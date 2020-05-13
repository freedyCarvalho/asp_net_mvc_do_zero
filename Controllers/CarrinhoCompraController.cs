using LanchesMacCurso.Models;
using LanchesMacCurso.Repositories;
using LanchesMacCurso.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LanchesMacCurso.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }
        
        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraViewModels = new CarrinhoCompraViewModels
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraViewModels);
        }

        // Usamos o RedirectToActionResult porque o usuário terá que confirmar o item no carrinho
        public RedirectToActionResult AdicionarItemNOCarinhoCompra(int lancheId)
        {
            
            // usamos o FirstOrDefault porque se ele não achar, é retornado null e não lança uma exceção
            // Se usamos o First() e ele não encontra, lança uma exceção e temos que tratar o erro
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado, 1);

            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoverItemDoCarrinhoCompra(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }
    }
}