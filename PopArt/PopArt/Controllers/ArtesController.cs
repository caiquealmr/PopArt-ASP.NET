using Microsoft.AspNetCore.Mvc;
using PopArt.Models;
using PopArt.Repositorio;
using System;
using System.Collections.Generic;

namespace PopArt.Controllers
{
    public class ArtesController : Controller
    {
        private readonly IPostagemRepositorio _postagemRepositorio;

        public ArtesController(IPostagemRepositorio postagemRepositorio)
        {
            _postagemRepositorio = postagemRepositorio;
        }

        public IActionResult Index()
        {
            try
            {
                // Busca todas as postagens (artes)
                var postagens = _postagemRepositorio.BuscarTodas();
                return View(postagens);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao carregar artes: {ex.Message}";
                return View(new List<PostagemModel>());
            }
        }
    }
}
