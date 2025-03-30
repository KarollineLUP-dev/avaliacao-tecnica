using Xunit;
using Microsoft.EntityFrameworkCore;
using ApiProduto.Data;
using ApiProduto.Models;
using System.Linq;

namespace ApiProduto.Tests.Data;

//Testes para o ProdutoDbContext
// Configuração da Precisão Decimal do Valor:
// Garantir que a precisão definida no OnModelCreating (18,2) foi aplicada corretamente ao campo Valor.
// Inserção e Recuperação de Dados:
// Criar um banco em memória usando UseInMemoryDatabase para testar se os dados podem ser inseridos e recuperados corretamente.
// Seed Data:
// Verificar se os dados inseridos na inicialização (HasData) são carregados corretamente.
// Operações CRUD:
// Criar, ler, atualizar e excluir um Produto no contexto para garantir que as operações funcionam corretamente.

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
    public void ProdutoDbContext_ConfiguracaoDeValor_DeveTerPrecisaoCorreta()
    {
        using var context = GetDbContext();
        var props = context.Model.FindEntityType(typeof(Produto))
            .FindProperty(nameof(Produto.Valor));

        Assert.Equal(18, props.GetPrecision());
        Assert.Equal(2, props.GetScale());
    }

    [Fact]
    public void ProdutoDbContext_SeedData_DeveConterProdutos()
    {
        var options = new DbContextOptionsBuilder<ProdutoDbContext>()
       .UseInMemoryDatabase(databaseName: "TestDatabase")
       .Options;

        using (var context = new ProdutoDbContext(options))
        {
            var produtos = context.Produtos.ToList();
            Assert.NotEmpty(produtos);
            Assert.Equal(5, produtos.Count);
        }
    }

    [Fact]
    public void ProdutoDbContext_Crud_DeveAdicionarLerAtualizarExcluirProduto()
    {
        using var context = GetDbContext();

        var produto = new Produto { Nome = "Teclado Mecânico", Estoque = 15, Valor = 300.00M };

        // Criar (Create)
        context.Produtos.Add(produto);
        context.SaveChanges();

        var produtoCriado = context.Produtos.FirstOrDefault(p => p.Nome == "Teclado Mecânico");
        Assert.NotNull(produtoCriado);

        // Atualizar (Update)
        produtoCriado.Valor = 350.00M;
        context.SaveChanges();

        var produtoAtualizado = context.Produtos.First(p => p.Nome == "Teclado Mecânico");
        Assert.Equal(350.00M, produtoAtualizado.Valor);

        // Excluir (Delete)
        context.Produtos.Remove(produtoAtualizado);
        context.SaveChanges();

        var produtoDeletado = context.Produtos.FirstOrDefault(p => p.Nome == "Teclado Mecânico");
        Assert.Null(produtoDeletado);
    }
}
