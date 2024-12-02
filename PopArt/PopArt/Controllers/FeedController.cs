using Microsoft.AspNetCore.Mvc;
using PopArt.Models;
using PopArt.Repositorio;
using System;
using System.Collections.Generic;

namespace PopArt.Controllers
{
    public class FeedController : Controller
    {
        private readonly IPostagemRepositorio _postagemRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public FeedController(IPostagemRepositorio postagemRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _postagemRepositorio = postagemRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            try
            {
                var postagens = _postagemRepositorio.BuscarTodas();
                var perfis = _usuarioRepositorio.BuscarPerfisAleatorios();

                var modelo = new Tuple<List<PostagemModel>, List<UsuarioModel>>(postagens, perfis);
                return View(modelo);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao carregar o feed: {ex.Message}";
                var modeloVazio = new Tuple<List<PostagemModel>, List<UsuarioModel>>(new List<PostagemModel>(), new List<UsuarioModel>());
                return View(modeloVazio);
            }
        }
    }
}
