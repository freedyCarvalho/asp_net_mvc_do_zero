using LanchesMacCurso.Models;
using LanchesMacCurso.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMacCurso.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(Pedido pedido)
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("","Seu Carrinho está vazio, inclua um lanche.");
            }

            if (ModelState.IsValid)
            {
                _pedidoRepository.CriarPedido(pedido);

                // Nâo está no curso, mas para não dar o erro "Serialize Decimal"
                // Já faço a conversão antes de enviar para o TempData e para o ViewBag
                decimal totalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                //TempData["Cliente"] = pedido.Nome;
                //TempData["NumeroPedido"] = pedido.PedidoId;
                //TempData["DataPedido"] = pedido.PedidoEnviado;
                //TempData["TotalPedido"] = totalPedido.ToString("C2");

                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :) ";
                ViewBag.TotalPedido = totalPedido.ToString("C2");

                _carrinhoCompra.LimparCarrinho();

                //return RedirectToAction("CheckoutCompleto");

                // implementação para exibição de mais informações sobre o pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            
            return View(pedido);
        }

        //public IActionResult CheckoutCompleto()
        //{
        //    ViewBag.Cliente = TempData["Cliente"];
        //    ViewBag.NumeroPedido = TempData["NumeroPedido"];
        //    ViewBag.DataPedido = TempData["DataPedido"];
        //    ViewBag.TotalPedido = TempData["TotalPedido"];

        //    ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido";

        //    return View();
        //}
    }
}
