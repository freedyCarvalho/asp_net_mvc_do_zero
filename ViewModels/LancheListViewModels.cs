using LanchesMacCurso.Models;
using System.Collections.Generic;

namespace LanchesMacCurso.ViewModels
{
    public class LancheListViewModels
    {
        public IEnumerable<Lanche> Lanches { get; set; }
        public string CategoriaAtual { get; set; }
    }
}
