using Microsoft.EntityFrameworkCore;
using ApiProduto.Models;

namespace ApiProduto.Data
{
    public class ProdutoDbContext : DbContext
    {
        public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options) { }

        public virtual DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .Property(p => p.Valor)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Notebook Asus", Estoque = 23, Valor = 3800.00M },
                new Produto { Id = 2, Nome = "Mouse Logitech", Estoque = 51, Valor = 150.00M },
                new Produto { Id = 3, Nome = "Teclado Logitech", Estoque = 19, Valor = 380.00M },
                new Produto { Id = 4, Nome = "Monitor AOC", Estoque = 15, Valor = 560.00M },
                new Produto { Id = 5, Nome = "Impressora Canon", Estoque = 3, Valor = 890.00M }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}