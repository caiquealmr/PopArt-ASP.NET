using PopArt.Models;

namespace PopArt.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);
        UsuarioModel BuscarSessaoDoUsuario();
        void RemoverSessaoDoUsuario();
    }
}
