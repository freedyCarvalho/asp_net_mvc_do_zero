using LanchesMacCurso.Context;
using LanchesMacCurso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMacCurso.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _contexto;

        public CategoriaRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Categoria> Categorias => _contexto.Categorias;
    }
}
