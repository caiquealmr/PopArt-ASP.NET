using Microsoft.AspNetCore.Mvc;
using PopArt.Models;
using PopArt.Repositorio;
using System;
using System.IO;

namespace PopArt.Controllers
{
    public class PostagemController : Controller
    {
        private readonly IPostagemRepositorio _postagemRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public PostagemController(IPostagemRepositorio postagemRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _postagemRepositorio = postagemRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(PostagemModel postagem, IFormFile imagemUpload)
        {
            try
            {
                // Validar usuário logado
                int usuarioId = _usuarioRepositorio.ObterIdUsuarioAtual();
                if (usuarioId == 0)
                {
                    TempData["MensagemErro"] = "Você precisa estar logado para criar uma postagem.";
                    return RedirectToAction("Index", "Login");
                }

                postagem.UsuarioId = usuarioId;
                postagem.DataCriacao = DateTime.Now;

                // Processar a imagem, se fornecida
                if (imagemUpload != null && imagemUpload.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        imagemUpload.CopyTo(ms);
                        postagem.Imagem = ms.ToArray();
                    }
                }

                // Salvar a postagem
                _postagemRepositorio.Criar(postagem);

                TempData["MensagemSucesso"] = "Postagem criada com sucesso!";
                return RedirectToAction("Index", "Perfil");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao criar a postagem: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Listar()
        {
            try
            {
                int usuarioId = _usuarioRepositorio.ObterIdUsuarioAtual();
                var postagens = _postagemRepositorio.BuscarPorUsuario(usuarioId);
                return View(postagens);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao carregar postagens: {ex.Message}";
                return RedirectToAction("Index", "Perfil");
            }
        }

        [HttpPost]
        public IActionResult Apagar(int id)
        {
            try
            {
                // Validar usuário logado
                int usuarioId = _usuarioRepositorio.ObterIdUsuarioAtual();
                if (usuarioId == 0)
                {
                    TempData["MensagemErro"] = "Você precisa estar logado para apagar uma postagem.";
                    return RedirectToAction("Index", "Login");
                }

                // Buscar a postagem
                var postagem = _postagemRepositorio.BuscarPorId(id);
                if (postagem == null || postagem.UsuarioId != usuarioId)
                {
                    TempData["MensagemErro"] = "Postagem não encontrada ou você não tem permissão para apagá-la.";
                    return RedirectToAction("Index", "Perfil");
                }

                // Apagar a postagem
                _postagemRepositorio.Apagar(id);
                TempData["MensagemSucesso"] = "Postagem apagada com sucesso!";
                return RedirectToAction("Index", "Perfil");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao apagar a postagem: {ex.Message}";
                return RedirectToAction("Index", "Perfil");
            }
        }

        public IActionResult Detalhes(int id)
        {
            try
            {
                var postagem = _postagemRepositorio.BuscarPorId(id);

                if (postagem == null)
                {
                    TempData["MensagemErro"] = "Postagem não encontrada.";
                    return RedirectToAction("Index", "Home");
                }

                return View(postagem);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao carregar detalhes da postagem: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
