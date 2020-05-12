using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanchesMacCurso.Context;
using LanchesMacCurso.Models;
using LanchesMacCurso.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
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

            // .AddTransient -> o servi�o ser� criado cada vez que for solicitado
            // .AddScope -> criado uma vez por solicita��o (cada requisi��o). Se duas pessoas solicitarem ao mesmo
            // tempo, cada uma ter� o seu carrinho e n�o teremos carrinhos duplicados
            // .AddSingleton -> Usado na primeira vez que for solicitado (todas as requisi��es tenho o mesmo objeto)
            // services.AddTransient<Interface, Implementa��o>
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ILancheRepository, LancheRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(cp => CarrinhoCompra.GetCarrinho(cp));
            services.AddMvc();
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
