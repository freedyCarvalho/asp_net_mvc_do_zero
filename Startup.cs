using LanchesMacCurso.Context;
using LanchesMacCurso.Models;
using LanchesMacCurso.Repositories;
using LanchesMacCurso.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LanchesMacCurso
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();

            // Registrar o AddIdentity que vai adicionar o Identity padr�o para os tipos de usu�rios e perfis especificados que v�o 
            // ser o IdentityUser e o IdentityRole. Depois, adicionamos o AddEntityFrameworkStore e referenciar o contexto da aplica��o
            // O AddEntityFramework adiciona uma implementa��o do EntityFrameWork que armazena as informa��es de entidade
            // Com o AddDefaultTokenProviders configuramos o servi�o do Identity para incluir a configura��o do sistema padr�o do Identity
            // para o usu�rio, representado em IdentityUser e o perfil em IdentityRole, usando o contexto (AppDbContext) do EntityFrameWorkCore
            // O AddDefaulttokenProviders vai incluir os tokens para troca de senha e envio de e-mails

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // .AddTransient -> o servi�o ser� criado cada vez que for solicitado
            // .AddScope -> criado uma vez por solicita��o (cada requisi��o). Se duas pessoas solicitarem ao mesmo
            // tempo, cada uma ter� o seu carrinho e n�o teremos carrinhos duplicados
            // .AddSingleton -> Usado na primeira vez que for solicitado (todas as requisi��es tenho o mesmo objeto)
            // services.AddTransient<Interface, Implementa��o>
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ILancheRepository, LancheRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(cp => CarrinhoCompra.GetCarrinho(cp));
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            // Recurso para ativar o redirecionamento de URL
            app.UseHttpsRedirection();
            
            // Recurso para acessar os arquivos est�ticos
            app.UseStaticFiles();

            // Ativamos a session usando o MiddleWare
            // No Asp Net Core tudo trabalha com o conceito de MiddleWare 
            // Precisamos ativar para usar o recurso. Se n�o ativamos, a aplica��o entende
            // que o recurso n�o existe
            app.UseSession();

            // Habilita para poder utilizar o Entity na aplica��o
            // Esse middleware vai adicionar a autentica��o ao Pipeline da solicita��o
            // Ele adiciona um �nico componente de middleware de autentica��o que � respons�vel pela autentica��o autom�tica
            // e pelo tratamento de pedidos de autentica��o remota. Ele vai substituir todos os componentes de middleware individuais
            // por um �nico componente de middleware comum.
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                   name: "CategoriaPorFiltro",
                   pattern: "Lanche/{action}/{categoria?}",
                   defaults: new { Controller = "Lanche", action = "List" });

               /*
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Lanche}/{action=List}/{id?}");
               */

               endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
               
            });

        }
    }
}
