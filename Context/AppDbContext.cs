using LanchesMacCurso.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMacCurso.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {

        }

        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

    }
}
