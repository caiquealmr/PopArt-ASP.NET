using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopArt.Models
{
    public class PostagemModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da arte é obrigatório")]
        public string Nome { get; set; }

        public byte[]? Imagem { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "A categoria é obrigatória")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "A descrição da arte é obrigatória")]
        public string Descricao { get; set; }

        // Chave estrangeira para o relacionamento com UsuarioModel
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
