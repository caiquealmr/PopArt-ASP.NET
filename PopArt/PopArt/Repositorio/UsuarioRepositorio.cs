using Microsoft.EntityFrameworkCore; // Necessário para o Include
using PopArt.Data;
using PopArt.Helper;
using PopArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PopArt.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        private readonly ISessao _sessao;

        public UsuarioRepositorio(BancoContext bancoContext, ISessao sessao)
        {
            _bancoContext = bancoContext;
            _sessao = sessao;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioModel BuscarPorIdComPostagens(int id)
        {
            return _bancoContext.Usuarios
                .Include(u => u.Postagens) // Inclui as postagens do usuário
                .FirstOrDefault(u => u.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public List<UsuarioModel> BuscarPerfisAleatorios()
        {
            return _bancoContext.Usuarios
                .OrderBy(u => Guid.NewGuid()) // Embaralha os perfis
                .Take(10) // Limita a 10 perfis
                .ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;

            if (string.IsNullOrEmpty(usuario.FotoDePerfil))
            {
                usuario.FotoDePerfil = "/images/default-profile.png";
            }

            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorId(usuario.Id);

            if (usuarioDB == null)
            {
                throw new Exception("Usuário não encontrado!");
            }

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.Biografia = usuario.Biografia;
            usuarioDB.LinkBehance = usuario.LinkBehance;
            usuarioDB.LinkLinkedIn = usuario.LinkLinkedIn;
            usuarioDB.LinkInstagram = usuario.LinkInstagram;
            usuarioDB.DataAtualizacao = DateTime.Now;

            if (!string.IsNullOrEmpty(usuario.FotoDePerfil))
            {
                usuarioDB.FotoDePerfil = usuario.FotoDePerfil;
            }

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = BuscarPorId(id);

            if (usuarioDB == null)
            {
                throw new Exception("Erro ao tentar apagar o usuário.");
            }

            _bancoContext.Usuarios.Remove(usuarioDB);
            _bancoContext.SaveChanges();
            return true;
        }

        public int ObterIdUsuarioAtual()
        {
            var usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            if (usuarioLogado == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }
            return usuarioLogado.Id;
        }
    }
}
