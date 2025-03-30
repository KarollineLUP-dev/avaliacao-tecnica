using Xunit;
using ApiProduto.Models;

namespace ApiProduto.Tests.Models;

//Validação de Propriedades:
// Criar um objeto Produto e garantir que as propriedades são atribuídas corretamente.
// Garantir que a propriedade Nome não pode ser nula ou vazia.
// Garantir que Estoque aceita apenas valores inteiros.
// Garantir que Valor aceita somente valores decimais positivos.
// Comparação de Objetos:
// Criar dois objetos Produto idênticos e verificar se são considerados iguais.

public class ProdutoTests
{
    [Fact]
    public void CriarProduto_PropriedadesDevemSerAtribuidasCorretamente()
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
    public void CriarProduto_ValorNegativo_DeveFalhar()
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
