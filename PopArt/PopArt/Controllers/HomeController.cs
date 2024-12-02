using Microsoft.AspNetCore.Mvc;
using PopArt.Models;
using PopArt.Repositorio;
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace PopArt.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostagemRepositorio _postagemRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public HomeController(IPostagemRepositorio postagemRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _postagemRepositorio = postagemRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            try
            {
                // Buscar postagens e perfis aleatórios para exibição
                var postagensAleatorias = _postagemRepositorio.BuscarPostagensAleatorias();
                var perfisAleatorios = _usuarioRepositorio.BuscarPerfisAleatorios();

                // Passar as listas para a View usando um Tuple
                var modelo = new Tuple<List<PostagemModel>, List<UsuarioModel>>(postagensAleatorias, perfisAleatorios);

                return View(modelo);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao carregar os dados: {ex.Message}";
                return View(new Tuple<List<PostagemModel>, List<UsuarioModel>>(new List<PostagemModel>(), new List<UsuarioModel>()));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
