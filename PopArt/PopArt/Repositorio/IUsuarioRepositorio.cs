using PopArt.Models;
using System.Collections.Generic;

namespace PopArt.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel BuscarPorId(int id);
        UsuarioModel BuscarPorIdComPostagens(int id); // Novo método
        List<UsuarioModel> BuscarTodos();
        List<UsuarioModel> BuscarPerfisAleatorios(); // Novo método para buscar perfis aleatórios
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Atualizar(UsuarioModel usuario);
        bool Apagar(int id);
        int ObterIdUsuarioAtual();
    }
}
