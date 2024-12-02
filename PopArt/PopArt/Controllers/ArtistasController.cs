using Microsoft.AspNetCore.Mvc;
using PopArt.Models;
using PopArt.Repositorio;
using System;
using System.Collections.Generic;

namespace PopArt.Controllers
{
    public class ArtistasController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public ArtistasController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            try
            {
                // Busca todos os perfis disponíveis
                var perfis = _usuarioRepositorio.BuscarTodos();
                return View(perfis);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao carregar artistas: {ex.Message}";
                return View(new List<UsuarioModel>());
            }
        }
    }
}
