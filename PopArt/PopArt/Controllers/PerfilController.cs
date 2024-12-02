using Microsoft.AspNetCore.Mvc;
using PopArt.Models;
using PopArt.Repositorio;
using System;
using System.Linq;

namespace PopArt.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPostagemRepositorio _postagemRepositorio;

        public PerfilController(IUsuarioRepositorio usuarioRepositorio, IPostagemRepositorio postagemRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _postagemRepositorio = postagemRepositorio;
        }

        public IActionResult Index()
        {
            try
            {
                // Obtém o ID do usuário atual
                int usuarioId = _usuarioRepositorio.ObterIdUsuarioAtual();
                if (usuarioId == 0)
                {
                    TempData["MensagemErro"] = "Você precisa estar logado para acessar o perfil.";
                    return RedirectToAction("Index", "Login");
                }

                // Buscar o usuário e as postagens associadas
                UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(usuarioId);
                if (usuario == null)
                {
                    TempData["MensagemErro"] = "Usuário não encontrado.";
                    return RedirectToAction("Index", "Login");
                }

                // Buscar as postagens do usuário
                var postagens = _postagemRepositorio.BuscarPorUsuario(usuarioId);
                usuario.Postagens = postagens.ToList();

                // Passar o modelo atualizado para a View
                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao carregar o perfil: {ex.Message}";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
