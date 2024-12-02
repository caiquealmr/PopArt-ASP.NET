using PopArt.Enums;
using System.ComponentModel.DataAnnotations;

namespace PopArt.Models
{

    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }

        public string? Biografia { get; set; }
        public string? FotoDePerfil { get; set; }
        public string? LinkBehance { get; set; }
        public string? LinkLinkedIn { get; set; }
        public string? LinkInstagram { get; set; }
    }
}
