using Microsoft.EntityFrameworkCore;

namespace ApiProduto.Data
{
    public class ProdutoDbContext : DbContext
    {
        public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .Property(p => p.Valor)
                .HasPrecision(18, 2); 

            base.OnModelCreating(modelBuilder);
        }
    }
}