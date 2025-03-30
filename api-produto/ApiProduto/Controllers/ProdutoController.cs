using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProduto.Data;
using ApiProduto.Models;
using ApiProduto.Dtos;

namespace ApiProduto.Controllers;
[Route("api/produtos")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoDbContext _context;

    public ProdutoController(ProdutoDbContext context)
    {
        _context = context;
    }

    [HttpGet("listar/")]
    public IActionResult GetAllLikeName([FromQuery] string? name = null)
    {
        IQueryable<Produto> produtos = _context.Produtos;

        if (!string.IsNullOrWhiteSpace(name))
        {
            produtos = produtos.Where(p => p.Nome.Contains(name));
            if (!produtos.Any())
            {
                return NotFound($"Nenhum produto encontrado com o nome '{name}'.");
            }
        }

        return Ok(produtos.ToList());
    }

    [HttpGet("ordenar/")]
    public IActionResult GetAllOrderBy([FromQuery] string? orderBy)
    {
        if (string.IsNullOrEmpty(orderBy))
            return BadRequest("O parâmetro de ordenação não pode ser nulo ou vazio.");

        orderBy = orderBy.ToLower();

        var colunasExistentes = new HashSet<string> { "nome", "estoque", "valor", "id" };

        if (!colunasExistentes.Contains(orderBy))
            return NotFound($"A coluna '{orderBy}' não existe para ordenação.");

        IQueryable<Produto> produtos = _context.Produtos;

        try
        {
            produtos = orderBy switch
            {
                "nome" => produtos.OrderBy(p => p.Nome),
                "estoque" => produtos.OrderBy(p => p.Estoque),
                "valor" => produtos.OrderBy(p => p.Valor),
                "id" => produtos.OrderBy(p => p.Id),
                _ => throw new ArgumentException($"A coluna '{orderBy}' não existe para ordenação.")
            };
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        return Ok(produtos.ToList());
    }

    [HttpGet("consultar")]
    public IActionResult GetByName([FromQuery] string name)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Nome == name);
        return produto == null ? NotFound() : Ok(produto);
    }

    [HttpGet("consultar/{id}")]
    public IActionResult GetById(int id)
    {
        var produto = _context.Produtos.Find(id);
        return produto == null ? NotFound() : Ok(produto);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProdutoDto produtoDto)
    {
        if (produtoDto.Valor < 0) return BadRequest("O valor do produto não pode ser negativo.");

        var produto = new Produto
        {
            Nome = produtoDto.Nome,
            Estoque = produtoDto.Estoque,
            Valor = produtoDto.Valor
        };

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ProdutoDto produtoDto)
    {
        if (produtoDto.Valor < 0)
            return BadRequest("O valor do produto não pode ser negativo.");

        var produto = _context.Produtos.Find(id);
        if (produto == null)
            return NotFound();

        produto.Nome = produtoDto.Nome;
        produto.Estoque = produtoDto.Estoque;
        produto.Valor = produtoDto.Valor;
        
        _context.SaveChanges();

        return GetById(id);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var produto = _context.Produtos.Find(id);
        if (produto == null) return NotFound();
        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return NoContent();
    }
}
