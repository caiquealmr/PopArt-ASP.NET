using Microsoft.AspNetCore.Mvc;
using PopArt.Models;
using PopArt.Repositorio;
using System;
using System.Collections.Generic;
using System.IO;

namespace PopArt.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index", "Login");
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao cadastrar o usuário. Detalhes: {erro.Message}";
                return View(usuario);
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);
                TempData["MensagemSucesso"] = apagado ? "Usuário apagado com sucesso!" : "Erro ao tentar apagar o usuário.";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao apagar o usuário. Detalhes: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            if (usuario == null)
            {
                TempData["MensagemErro"] = "Usuário não encontrado.";
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel, IFormFile? novaFoto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioDB = _usuarioRepositorio.BuscarPorId(usuarioSemSenhaModel.Id);

                    if (usuarioDB == null)
                    {
                        TempData["MensagemErro"] = "Usuário não encontrado.";
                        return RedirectToAction("Index");
                    }

                    // Atualizar os dados do usuário
                    usuarioDB.Nome = usuarioSemSenhaModel.Nome;
                    usuarioDB.Login = usuarioSemSenhaModel.Login;
                    usuarioDB.Email = usuarioSemSenhaModel.Email;
                    usuarioDB.Perfil = usuarioSemSenhaModel.Perfil;
                    usuarioDB.Biografia = usuarioSemSenhaModel.Biografia;
                    usuarioDB.LinkBehance = usuarioSemSenhaModel.LinkBehance;
                    usuarioDB.LinkLinkedIn = usuarioSemSenhaModel.LinkLinkedIn;
                    usuarioDB.LinkInstagram = usuarioSemSenhaModel.LinkInstagram;
                    usuarioDB.DataAtualizacao = DateTime.Now;

                    // Atualizar a foto apenas se uma nova for enviada
                    if (novaFoto != null && novaFoto.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(usuarioDB.FotoDePerfil) && usuarioDB.FotoDePerfil != "/images/default-profile.png")
                        {
                            var caminhoAntigo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", usuarioDB.FotoDePerfil.TrimStart('/'));
                            if (System.IO.File.Exists(caminhoAntigo))
                            {
                                System.IO.File.Delete(caminhoAntigo);
                            }
                        }

                        var caminhoFoto = SalvarFoto(novaFoto);
                        usuarioDB.FotoDePerfil = caminhoFoto;
                    }

                    // Persistir os dados no banco
                    _usuarioRepositorio.Atualizar(usuarioDB);

                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
                    return RedirectToAction("Index", "Perfil");
                }

                // Recarregar o modelo original em caso de erro de validação
                var usuarioRecarregado = _usuarioRepositorio.BuscarPorId(usuarioSemSenhaModel.Id);
                return View(usuarioRecarregado);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar o usuário. Detalhes: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult PerfilPublico(int id)
        {
            var usuario = _usuarioRepositorio.BuscarPorIdComPostagens(id);

            if (usuario == null)
            {
                TempData["MensagemErro"] = "Usuário não encontrado.";
                return RedirectToAction("Index", "Home");
            }

            return View(usuario);
        }

        private string SalvarFoto(IFormFile foto)
        {
            var formatosPermitidos = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            if (!formatosPermitidos.Contains(Path.GetExtension(foto.FileName).ToLower()))
            {
                throw new Exception("Formato de foto inválido. Use .jpg, .jpeg, .png ou .gif.");
            }

            var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "perfil");
            var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);

            if (!Directory.Exists(pastaDestino))
            {
                Directory.CreateDirectory(pastaDestino);
            }

            var caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                foto.CopyTo(stream);
            }

            return $"/images/perfil/{nomeArquivo}";
        }
    }
}
