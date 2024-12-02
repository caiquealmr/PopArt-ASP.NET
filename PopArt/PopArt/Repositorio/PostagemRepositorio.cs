using Microsoft.EntityFrameworkCore;
using PopArt.Data;
using PopArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PopArt.Repositorio
{
    public class PostagemRepositorio : IPostagemRepositorio
    {
        private readonly BancoContext _bancoContext;

        public PostagemRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        // Implementação: Buscar todas as postagens
        public List<PostagemModel> BuscarTodas()
        {
            return _bancoContext.Postagens
                .Include(p => p.Usuario) // Inclui informações do artista
                .ToList();
        }

        // Implementação: Buscar postagens por usuário
        public List<PostagemModel> BuscarPorUsuario(int usuarioId)
        {
            return _bancoContext.Postagens
                .Where(p => p.UsuarioId == usuarioId)
                .Include(p => p.Usuario) // Inclui informações do artista
                .ToList();
        }

        // Implementação: Buscar postagens aleatórias
        public List<PostagemModel> BuscarPostagensAleatorias()
        {
            return _bancoContext.Postagens
                .OrderBy(p => Guid.NewGuid()) // Ordenação aleatória
                .Include(p => p.Usuario) // Inclui informações do artista
                .Take(10) // Limite de 10 postagens
                .ToList();
        }

        // Implementação: Criar uma nova postagem
        public void Criar(PostagemModel postagem)
        {
            _bancoContext.Postagens.Add(postagem);
            _bancoContext.SaveChanges();
        }

        // Implementação: Buscar uma postagem por ID
        public PostagemModel BuscarPorId(int id)
        {
            return _bancoContext.Postagens
                .Include(p => p.Usuario) // Inclui informações do artista
                .FirstOrDefault(p => p.Id == id);
        }

        // Implementação: Apagar uma postagem por ID
        public void Apagar(int id)
        {
            var postagem = BuscarPorId(id);
            if (postagem != null)
            {
                _bancoContext.Postagens.Remove(postagem);
                _bancoContext.SaveChanges();
            }
        }
    }
}
