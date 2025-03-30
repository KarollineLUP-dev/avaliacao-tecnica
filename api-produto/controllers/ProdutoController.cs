using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProduto.Data;

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
    public IActionResult GetProdutosLikeName([FromQuery] string? name = null)
    {
        IQueryable<Produto> produtos = _context.Produtos;

        if (!string.IsNullOrWhiteSpace(name))
        {
            produtos = produtos.Where(p => p.Nome.Contains(name));
        }

        return Ok(produtos.ToList());
    }

    [HttpGet("ordenar/")]
    public IActionResult GetProdutosOrderBy([FromQuery] string orderBy = null)
    {
        IQueryable<Produto> produtos = _context.Produtos;

        produtos = orderBy.ToLower() switch
        {
            "nome" => produtos.OrderBy(p => p.Nome),
            "estoque" => produtos.OrderBy(p => p.Estoque),
            "valor" => produtos.OrderBy(p => p.Valor),
            _ => produtos.OrderBy(p => p.Id)
        };

        return Ok(produtos.ToList());
    }

    [HttpGet("consultar/{id}")]
    public IActionResult GetById(int id)
    {
        var produto = _context.Produtos.Find(id);
        return produto == null ? NotFound() : Ok(produto);
    }

    [HttpGet("consultar")]
    public IActionResult GetByName([FromQuery] string name)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Nome == name);
        return produto == null ? NotFound() : Ok(produto);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Produto produto)
    {
        if (produto.Valor < 0) return BadRequest("O valor do produto não pode ser negativo.");
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Produto produto)
    {
        var existingProduct = _context.Produtos.Find(id);
        if (existingProduct == null) return NotFound();

        existingProduct.Nome = produto.Nome;
        existingProduct.Estoque = produto.Estoque;
        existingProduct.Valor = produto.Valor;
        _context.SaveChanges();
        return NoContent();
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
