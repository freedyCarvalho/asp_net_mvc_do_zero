using LanchesMacCurso.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanchesMacCurso.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _contexto;

        public CarrinhoCompra(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        // Estamos usando o tipo string porque vamos usar o identificador único
        // global, o struct Guid, que possui um valor de 16 bytes que, se gerado
        // randomicamente, irá gerar um identificador (quase) único.
        // como vamos trabalhar com essa classe em memória, não vamos usar os recursos
        // do Entity Framework para gerar uma chave primária para inserir em uma tabela
        public string CarrinhoCompraId { get; set; }

        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho(System.IServiceProvider services)
        {
            // define uma sessão acessando o contexto atual (tem que registrar em IServicesCo
            // Vai retornar uma session se o operador <IHttpContextAcessor> não for nulo
            // por isso usamos o operador condicional nulo ?.
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // obtém um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            // obtém ou gera o Id do carrinho
            // usamos o operador de coalicence nula (??) 
            // se a sessão do carrinho "sessionGetString("CarrinhoId") existir, mantém o valor
            // se for nula, o comando Guid.NewGuid() irá atribuir um novo valor único para a sessão do carrinho
            // gera um número aleatório de 128 bits
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // atribui o id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            // retorna o carrinho com o contexto atual e o Id atribuído ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche, int quantidade)
        {
            var carrinhoCompraItem = _contexto.CarrinhoCompraItens.SingleOrDefault(s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            // verifica se o carrinho existe e se não, cria um
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _contexto.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else // se existir o carrinho com o item, então incrementa a quantidade
            {
                carrinhoCompraItem.Quantidade++;
            }

            _contexto.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _contexto.CarrinhoCompraItens.SingleOrDefault(s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
                quantidadeLocal = carrinhoCompraItem.Quantidade;
            }
            else
            {
                _contexto.CarrinhoCompraItens.Remove(carrinhoCompraItem);
            }

            _contexto.SaveChanges();

            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItens ?? 
                (CarrinhoCompraItens = _contexto.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Include(s => s.Lanche)
                .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _contexto.CarrinhoCompraItens.Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _contexto.CarrinhoCompraItens.RemoveRange(carrinhoItens);

            _contexto.SaveChanges();

        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _contexto.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

            return total;
        }
    }
}
