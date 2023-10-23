using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Data;
using MinhaAPI.Models;

namespace MinhaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly LojaDbContext _context;

        public EstoqueController(LojaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("listar_Produtos")]
        public async Task<ActionResult<IEnumerable<Produto>>> Listar()
        {
            if (_context is null) return NotFound();
            if (_context.Produtos is null) return NotFound();
            return await _context.Produtos.ToListAsync();
        }

        [HttpGet]
        [Route("buscar/{produto}")]
        public async Task<ActionResult<Produto>> Buscar(int produtoId)
        {
            if (_context is null) return NotFound();
            if (_context.Produtos is null) return NotFound();
            var produtoTemp = await _context.Produtos.FindAsync(produtoId);
            if (produtoTemp is null) return NotFound();
            return produtoTemp;
        }

        [HttpPut()]
        [Route("alterarProduto")]
        public async Task<ActionResult> Alterar(Produto produto)
        {
            if (_context is null) return NotFound();
            if (_context.Produtos is null) return NotFound();
            var produtoTemp = await _context.Produtos.FindAsync(produto.IdProduto);
            if (produtoTemp is null) return NotFound();
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch()]
        [Route("removerProduto/{produto}")]
        public async Task<ActionResult> RemoverProduto(int produtoId)
        {
            if (_context is null) return NotFound();
            if (_context.Produtos is null) return NotFound();
            var produtoTemp = await _context.Produtos.FindAsync(produtoId);
            if (produtoTemp is null) return NotFound();
            _context.Produtos.Remove(produtoTemp);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
