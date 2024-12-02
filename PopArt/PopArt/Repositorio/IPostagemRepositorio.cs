using PopArt.Models;
using System.Collections.Generic;

namespace PopArt.Repositorio
{
    public interface IPostagemRepositorio
    {
        // Buscar todas as postagens
        List<PostagemModel> BuscarTodas();

        // Buscar postagens de um usuário específico
        List<PostagemModel> BuscarPorUsuario(int usuarioId);

        // Buscar postagens aleatórias
        List<PostagemModel> BuscarPostagensAleatorias();

        // Criar uma nova postagem
        void Criar(PostagemModel postagem);

        // Buscar uma postagem por ID
        PostagemModel BuscarPorId(int id);

        // Apagar uma postagem por ID
        void Apagar(int id);
    }
}
