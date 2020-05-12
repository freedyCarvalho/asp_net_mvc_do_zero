using Microsoft.EntityFrameworkCore.Migrations;

namespace LanchesMacCurso.Migrations
{
    public partial class PopularTabelasv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into Categorias (CategoriaNome, Descricao) Values ('Normal','Lanche feito com ingredientes normais')");
            migrationBuilder.Sql("Insert Into Categorias (CategoriaNome, Descricao) Values ('Natural','Lanche feito com ingredientes integrais e naturais')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From Categorias");
        }
    }
}
