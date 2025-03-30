using Xunit;
using ApiProduto.Models;
using System;

namespace ApiProduto.Tests.Models;

public class ProdutoTests
{
    [Fact]
    public void CriarProduto_PropriedadesAtribuidasCorretamente()
    {
        var produto = new Produto
        {
            Id = 3,
            Nome = "Notebook Dell",
            Estoque = 10,
            Valor = 4500.00M
        };

        Assert.Equal(3, produto.Id);
        Assert.Equal("Notebook Dell", produto.Nome);
        Assert.Equal(10, produto.Estoque);
        Assert.Equal(4500.00M, produto.Valor);
    }

    [Fact]
    public void CriarProduto_NomeNulo_RetornaErro()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        new Produto
        {
            Nome = null!,
            Estoque = 5,
            Valor = 100.00M
        });

        Assert.Equal("O nome do produto não pode ser nulo ou vazio.", exception.Message);
    }

    [Fact]
    public void CriarProduto_NomeVazio_RetornaErro()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        new Produto
        {
            Nome = "",
            Estoque = 5,
            Valor = 100.00M
        });

        Assert.Equal("O nome do produto não pode ser nulo ou vazio.", exception.Message);
    }

    [Fact]
    public void CriarProduto_ValorNegativo_RetornaErro()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        new Produto
        {
            Nome = "Monitor",
            Estoque = 5,
            Valor = -100.00M
        });

        Assert.Equal("O valor do produto não pode ser negativo.", exception.Message);
    }

    [Fact]
    public void CriarProduto_EstoqueNegativo_RetornaErro()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        new Produto
        {
            Nome = "Mouse",
            Estoque = -10,
            Valor = 50.00M
        });

        Assert.Equal("O estoque do produto não pode ser negativo.", exception.Message);
    }

    [Fact]
    public void CriarProduto_EstoqueAceitaNumerosInteiros()
    {
        var produto = new Produto
        {
            Nome = "Cadeira Gamer",
            Estoque = 15,
            Valor = 800.00M
        };

        Assert.IsType<int>(produto.Estoque);
    }

    [Fact]
    public void CriarProduto_SemId_RetornaSuccess()
    {
        var produto = new Produto
        {
            Nome = "Teclado RGB",
            Estoque = 20,
            Valor = 200.00M
        };

        Assert.True(produto.Id == 0 || produto.Id > 0);
    }

    [Fact]
    public void CriarProduto_ValoresAltos_RetornaSuccess()
    {
        var produto = new Produto
        {
            Nome = "Mouse Dell",
            Estoque = int.MaxValue,
            Valor = decimal.MaxValue
        };

        Assert.Equal(int.MaxValue, produto.Estoque);
        Assert.Equal(decimal.MaxValue, produto.Valor);
    }

    [Fact]
    public void CompararProdutos_ProdutosIguaisDevemSerConsideradosIguais()
    {
        var produto1 = new Produto { Id = 1, Nome = "Mouse", Estoque = 5, Valor = 100.00M };
        var produto2 = new Produto { Id = 1, Nome = "Mouse", Estoque = 5, Valor = 100.00M };

        Assert.Equal(produto1.Id, produto2.Id);
        Assert.Equal(produto1.Nome, produto2.Nome);
        Assert.Equal(produto1.Estoque, produto2.Estoque);
        Assert.Equal(produto1.Valor, produto2.Valor);
    }
}
