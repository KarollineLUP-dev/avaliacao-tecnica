using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ApiProduto.Data;
using ApiProduto.Models;
using ApiProduto.Dtos;
using System.Linq;

namespace ApiProduto.Tests.Data;

public class ProdutoDbContextTests
{
    private ProdutoDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ProdutoDbContext>()
            .UseInMemoryDatabase(databaseName: "ProdutoTestDB")
            .Options;
        return new ProdutoDbContext(options);
    }

    [Fact]
    public void ProdutoDbContext_ValorDecimal_PrecisaoCorreta()
    {
        using var context = GetDbContext();

        var entityType = context.Model.FindEntityType(typeof(Produto));
        Assert.NotNull(entityType);

        var props = entityType!.FindProperty(nameof(Produto.Valor));
        Assert.NotNull(props);

        Assert.Equal(18, props!.GetPrecision());
        Assert.Equal(2, props.GetScale());
    }

    [Fact]
    public void ProdutoDbContext_SeedData_DeveConterProdutos()
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var options = new DbContextOptionsBuilder<ProdutoDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        Assert.False(string.IsNullOrEmpty(connectionString), "A connection string não foi encontrada no appsettings.json.");

        using (var context = new ProdutoDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var produtos = context.Produtos.ToList();

            Assert.NotEmpty(produtos);
        }
    }

    [Fact]
    public void ProdutoDbContext_Crud_AdicionarLerAtualizarExcluirProduto()
    {
        using var context = GetDbContext();

        var produto = new Produto { Nome = "Teclado Mecânico", Estoque = 15, Valor = 300.00M };

        context.Produtos.Add(produto);
        context.SaveChanges();

        var produtoCriado = context.Produtos.FirstOrDefault(p => p.Nome == "Teclado Mecânico");
        Assert.NotNull(produtoCriado);

        produtoCriado.Valor = 350.00M;
        context.SaveChanges();

        var produtoAtualizado = context.Produtos.First(p => p.Nome == "Teclado Mecânico");
        Assert.Equal(350.00M, produtoAtualizado.Valor);

        context.Produtos.Remove(produtoAtualizado);
        context.SaveChanges();

        var produtoDeletado = context.Produtos.FirstOrDefault(p => p.Nome == "Teclado Mecânico");
        Assert.Null(produtoDeletado);
    }

    [Fact]
    public void ProdutoDbContext_Crud_RetornaErroAoAdicionarProdutoNomeNulo()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            var produto = new Produto { Nome = null!, Estoque = 10, Valor = 100.00M };
        });

        Assert.Equal("O nome do produto não pode ser nulo ou vazio.", exception.Message);
    }

    [Fact]
    public void ProdutoDbContext_Crud_RetornaErroAoAdicionarProdutoValorNegativo()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            var produto = new Produto { Nome = "Monitor", Estoque = 5, Valor = -50.00M };
        });

        Assert.Equal("O valor do produto não pode ser negativo.", exception.Message);
    }
}
