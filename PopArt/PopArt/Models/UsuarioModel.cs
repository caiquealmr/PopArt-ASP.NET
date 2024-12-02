using PopArt.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopArt.Models
{
    public class UsuarioModel
    {
        [Key] // Define a chave primária
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário")]
        [StringLength(100, ErrorMessage = "O nome do usuário pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário")]
        [StringLength(50, ErrorMessage = "O login pode ter no máximo 50 caracteres.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }

        [Required(ErrorMessage = "Digite a senha do usuário")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Senha { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? DataAtualizacao { get; set; }

        [StringLength(500, ErrorMessage = "A biografia pode ter no máximo 500 caracteres.")]
        public string? Biografia { get; set; }

        // Alterado para armazenar caminho da imagem em vez de byte[]
        [StringLength(255, ErrorMessage = "O caminho da foto pode ter no máximo 255 caracteres.")]
        public string? FotoDePerfil { get; set; }

        [Url(ErrorMessage = "O link do Behance é inválido.")]
        public string? LinkBehance { get; set; }

        [Url(ErrorMessage = "O link do LinkedIn é inválido.")]
        public string? LinkLinkedIn { get; set; }

        [Url(ErrorMessage = "O link do Instagram é inválido.")]
        public string? LinkInstagram { get; set; }

        // Relacionamento com as postagens
        public ICollection<PostagemModel> Postagens { get; set; } = new List<PostagemModel>();

        // Método para validar senha
        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }
    }
}
