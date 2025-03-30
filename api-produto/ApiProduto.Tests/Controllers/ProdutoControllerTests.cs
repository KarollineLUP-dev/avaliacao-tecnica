using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ApiProduto.Data;
using System.Threading.Tasks;
using ApiProduto.Controllers;
using ApiProduto.Models;

namespace ApiProduto.Tests.Controllers;

public class ProdutoControllerTests
{
    private ProdutoController _controller;
    private Mock<DbSet<Produto>> _mockSet;
    private Mock<ProdutoDbContext> _mockContext;

    public ProdutoControllerTests()
    {
        var produtos = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Notebook Asus", Estoque = 23, Valor = 3800.00M },
            new Produto { Id = 2, Nome = "Mouse Logitech", Estoque = 51, Valor = 150.00M }
        }.AsQueryable();

        _mockSet = new Mock<DbSet<Produto>>();
        _mockSet.As<IQueryable<Produto>>().Setup(m => m.Provider).Returns(produtos.Provider);
        _mockSet.As<IQueryable<Produto>>().Setup(m => m.Expression).Returns(produtos.Expression);
        _mockSet.As<IQueryable<Produto>>().Setup(m => m.ElementType).Returns(produtos.ElementType);
        _mockSet.As<IQueryable<Produto>>().Setup(m => m.GetEnumerator()).Returns(produtos.GetEnumerator());

        _mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
         .Returns((object[] ids) => produtos.FirstOrDefault(p => p.Id == (int)ids[0]));

        var options = new DbContextOptionsBuilder<ProdutoDbContext>().Options;
        _mockContext = new Mock<ProdutoDbContext>(options);
        _mockContext.Setup(c => c.Produtos).Returns(_mockSet.Object);

        _controller = new ProdutoController(_mockContext.Object);
    }

    [Fact]
    public void GetAllOrderBy_ParametroExiste_RetornaProdutosOrdenados()
    {
        string orderBy = "Valor";

        var result = _controller.GetAllOrderBy(orderBy);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var produtos = Assert.IsType<List<Produto>>(okResult.Value);
        var produtosOrdenados = produtos.OrderBy(p => p.Valor).ToList();

        Assert.NotEmpty(produtos);
        Assert.Equal(produtosOrdenados, produtos);
    }

    [Fact]
    public void GetAllOrderBy_ParametroNaoExiste_RetornaNotFound()
    {
        string orderBy = "Fonecedor";

        var result = _controller.GetAllOrderBy(orderBy);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetAllOrderBy_ParametroVazio_RetornaBadRequest()
    {
        string? orderBy = null;

        var result = _controller.GetAllOrderBy(orderBy);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetAllLikeName_ProdutoExiste_RetornaProdutos()
    {
        string produtoNome = "Mouse";

        var result = _controller.GetAllLikeName(produtoNome);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var produtos = Assert.IsType<List<Produto>>(okResult.Value);

        Assert.NotEmpty(produtos);
        Assert.Contains(produtos, p => p.Nome.Contains(produtoNome));
    }

    [Fact]
    public void GetAllLikeName_NomeVazio_RetornaTodosProdutos()
    {
        string? produtoNome = null;

        var result = _controller.GetAllLikeName(produtoNome);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var produtos = Assert.IsType<List<Produto>>(okResult.Value);

        Assert.NotEmpty(produtos);
    }

    [Fact]
    public void GetAllLikeName_ProdutoNaoExiste_RetornaNotFound()
    {
        string produtoNome = "Webcam";

        var result = _controller.GetAllLikeName(produtoNome);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetById_ProdutoExiste_RetornaProduto()
    {
        int produtoId = 1;

        var result = Assert.IsType<OkObjectResult>(_controller.GetById(produtoId));
        var produto = Assert.IsType<Produto>(result.Value);

        Assert.Equal(produtoId, produto.Id);
    }

    [Fact]
    public void GetById_ProdutoNaoExiste_RetornaNotFound()
    {
        _mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
        .Returns((object[] ids) => null);

        var result = _controller.GetById(99);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void GetByName_ProdutoExiste_RetornaProduto()
    {
        string produtoNome = "Mouse Logitech";

        var result = Assert.IsType<OkObjectResult>(_controller.GetByName(produtoNome));
        var produto = Assert.IsType<Produto>(result.Value);

        Assert.Equal(produtoNome, produto.Nome);
    }

    [Fact]
    public void GetByName_ProdutoNaoExiste_RetornaNotFound()
    {
        _mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
        .Returns((object[] ids) => null);

        var result = _controller.GetByName("Mouse");

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Post_ProdutoValido_RetornaCreated()
    {
        var produto = new Produto { Nome = "Teclado Multilaser", Estoque = 10, Valor = 200.00M };
        var result = Assert.IsType<CreatedAtActionResult>(_controller.Post(produto));

        Assert.Equal(nameof(_controller.GetById), result.ActionName);

        var createdProduto = Assert.IsType<Produto>(result.Value);
        Assert.Equal("Teclado Multilaser", createdProduto.Nome);
    }

    [Fact]
    public void Post_ProdutoComValorNegativo_RetornaBadRequest()
    {
        var produto = new Produto { Nome = "Webcam Logitech", Estoque = 10, Valor = -10.00M };
        var result = Assert.IsType<BadRequestObjectResult>(_controller.Post(produto));

        Assert.Equal("O valor do produto não pode ser negativo.", result.Value);
    }

    [Fact]
    public void Put_ProdutoValido_RetornaProdutoAtualizado()
    {
        int produtoId = 1;
        var produto = new Produto { Nome = "Teclado Multilaser", Estoque = 10, Valor = 200.00M };

        var result = _controller.Put(produtoId, produto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var updatedProduto = Assert.IsType<Produto>(okResult.Value);

        Assert.Equal("Teclado Multilaser", updatedProduto.Nome);
        Assert.Equal(10, updatedProduto.Estoque);
        Assert.Equal(200.00M, updatedProduto.Valor);
    }

    [Fact]
    public void Put_ProdutoComValorNegativo_RetornaBadRequest()
    {
        int produtoId = 2;
        var produto = new Produto { Nome = "Mouse Multilaser", Estoque = 10, Valor = -10.00M };

        var result = Assert.IsType<BadRequestObjectResult>(_controller.Put(produtoId, produto));

        Assert.Equal("O valor do produto não pode ser negativo.", result.Value);
    }

    [Fact]
    public void Put_IdNaoExiste_RetornaNotFound()
    {
        int produtoId = 99;
        var produto = new Produto { Nome = "Teclado Multilaser", Estoque = 10, Valor = 200.00M };

        var result = _controller.Put(produtoId, produto);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_IdExiste_RetornaSuccess()
    {
        int produtoId = 1;

        var result = _controller.Delete(produtoId);

        var okResult = Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Delete_IdNaoExiste_RetornaNotFound()
    {
        int produtoId = 99;

        var result = _controller.Delete(produtoId);

        Assert.IsType<NotFoundResult>(result);
    }
}
