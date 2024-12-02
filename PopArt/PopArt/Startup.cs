using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PopArt.Data;
using PopArt.Helper;
using PopArt.Repositorio;

namespace PopArt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurando o contexto do banco de dados e a conexão com o SQL Server
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<BancoContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DataBase")));

            // Registrando os repositórios para injeção de dependência
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IPostagemRepositorio, PostagemRepositorio>();  // Adicionando o repositório de postagens
            services.AddScoped<ISessao, Sessao>();  // Registrando a sessão

            // Habilitando o acesso ao IHttpContextAccessor (necessário para a Sessao)
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Habilitando o uso de sessões para persistir dados entre as requisições
            services.AddSession(o =>
            {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });

            // Configurando o MVC para utilizar Controllers e Views
            services.AddControllersWithViews();

            // Configuração adicional para uploads (opcional)
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 209715200; // Aumentar o limite de upload para 200MB
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();  // Forçar o uso de HTTPS
            app.UseStaticFiles();  // Permitir servir arquivos estáticos (como imagens)

            app.UseRouting();  // Habilitar roteamento de URLs

            app.UseAuthorization();  // Habilitar o middleware de autorização
            app.UseSession();  // Habilitar sessões (deve ser após o UseRouting)

            // Definir os endpoints e as rotas
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
