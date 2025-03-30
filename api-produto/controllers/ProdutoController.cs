using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
[Route("api/produtos")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoDbContext _context;

    public ProdutoController(ProdutoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_context.Produtos.ToList());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var produto = _context.Produtos.Find(id);
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
