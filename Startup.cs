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

            // Registrar o AddIdentity que vai adicionar o Identity padrão para os tipos de usuários e perfis especificados que vão 
            // ser o IdentityUser e o IdentityRole. Depois, adicionamos o AddEntityFrameworkStore e referenciar o contexto da aplicação
            // O AddEntityFramework adiciona uma implementação do EntityFrameWork que armazena as informações de entidade
            // Com o AddDefaultTokenProviders configuramos o serviço do Identity para incluir a configuração do sistema padrão do Identity
            // para o usuário, representado em IdentityUser e o perfil em IdentityRole, usando o contexto (AppDbContext) do EntityFrameWorkCore
            // O AddDefaulttokenProviders vai incluir os tokens para troca de senha e envio de e-mails

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // .AddTransient -> o serviço será criado cada vez que for solicitado
            // .AddScope -> criado uma vez por solicitação (cada requisição). Se duas pessoas solicitarem ao mesmo
            // tempo, cada uma terá o seu carrinho e não teremos carrinhos duplicados
            // .AddSingleton -> Usado na primeira vez que for solicitado (todas as requisições tenho o mesmo objeto)
            // services.AddTransient<Interface, Implementação>
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
            
            // Recurso para acessar os arquivos estáticos
            app.UseStaticFiles();

            // Ativamos a session usando o MiddleWare
            // No Asp Net Core tudo trabalha com o conceito de MiddleWare 
            // Precisamos ativar para usar o recurso. Se não ativamos, a aplicação entende
            // que o recurso não existe
            app.UseSession();

            // Habilita para poder utilizar o Entity na aplicação
            // Esse middleware vai adicionar a autenticação ao Pipeline da solicitação
            // Ele adiciona um único componente de middleware de autenticação que é responsável pela autenticação automática
            // e pelo tratamento de pedidos de autenticação remota. Ele vai substituir todos os componentes de middleware individuais
            // por um único componente de middleware comum.
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
