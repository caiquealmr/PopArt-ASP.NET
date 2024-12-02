using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PopArt.Models;
using System.Threading.Tasks;

namespace PopArt.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            // Verifica se o usuário está acessando a página de cadastro
            string currentAction = HttpContext.Request.RouteValues["action"]?.ToString();
            string currentController = HttpContext.Request.RouteValues["controller"]?.ToString();

            // Permite a visualização da página de cadastro mesmo que o usuário não esteja logado
            if (currentController == "Usuario" && currentAction == "Criar")
            {
                // Retorna uma View sem o usuário logado
                return View("MenuDeslogado"); // Crie uma view chamada "AnonymousMenu"
            }

            // Verifica se há uma sessão ativa de usuário
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                // Retorna uma View sem o usuário logado
                return View("MenuDeslogado"); // Crie uma view chamada "AnonymousMenu"
            }

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
            return View(usuario); // Retorna o menu normal com os dados do usuário logado
        }
    }
}
