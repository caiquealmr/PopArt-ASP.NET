using Microsoft.EntityFrameworkCore;
using PopArt.Models;

namespace PopArt.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<PostagemModel> Postagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento entre Postagem e Usuario
            modelBuilder.Entity<PostagemModel>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Postagens)
                .HasForeignKey(p => p.UsuarioId);
        }
    }
}
