using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Data;
using MinhaAPI.Models;

namespace MinhaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        private readonly LojaDbContext _context;

        public CarrinhoController(LojaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Cadastrar_Carrinho")]
        public async Task<ActionResult> Cadastrar_Carrinho(Carrinho carrinho)
        {
            if (_context is null) return NotFound();
            if (_context.Carrinhos is null) return NotFound();
            await _context.Carrinhos.AddRangeAsync(carrinho);
            await _context.SaveChangesAsync();
            return Created("", carrinho);
        }

        [HttpGet]
        [Route("listar_Carrinhos")]
        public async Task<ActionResult<IEnumerable<Carrinho>>> Listar()
        {
            if (_context is null) return NotFound();
            if (_context.Carrinhos is null) return NotFound();
            return await _context.Carrinhos.ToListAsync();
        }

        [HttpGet]
        [Route("buscar/{carrinho}")]
        public async Task<ActionResult<Carrinho>> Buscar(int carrinhoId)
        {
            if (_context is null) return NotFound();
            if (_context.Carrinhos is null) return NotFound();
            var carrinhoTemp = await _context.Carrinhos.FindAsync(carrinhoId);
            if (carrinhoTemp is null) return NotFound();
            return carrinhoTemp;
        }

        [HttpPut()]
        [Route("alterarCarrinho")]
        public async Task<ActionResult> Alterar(Carrinho carrinho)
        {
            if (_context is null) return NotFound();
            if (_context.Carrinhos is null) return NotFound();
            var carrinhoTemp = await _context.Carrinhos.FindAsync(carrinho.IdCarrinho);
            if (carrinhoTemp is null) return NotFound();
            _context.Carrinhos.Update(carrinho);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch()]
        [Route("adicionarProduto/{carrinho}/{produto}")]
        public async Task<ActionResult> AdicionarProduto(int carrinhoId, int produtoId)
        {
            if (_context is null) return NotFound();
            if (_context.Carrinhos is null) return NotFound();
            if (_context.Produtos is null) return NotFound();

            var carrinhoTemp = await _context.Carrinhos.FindAsync(carrinhoId);
            if (carrinhoTemp is null) return NotFound();

            var produtoTemp = await _context.Produtos.FindAsync(produtoId);
            if (produtoTemp is null) return NotFound();

            carrinhoTemp.ItensCarrinho.Add(produtoTemp);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch()]
        [Route("removerProduto/{carrinho}/{produto}")]
        public async Task<ActionResult> RemoverProduto(int carrinhoId, int produtoId)
        {
            if (_context is null) return NotFound();
            if (_context.Carrinhos is null) return NotFound();
            if (_context.Produtos is null) return NotFound();

            var carrinhoTemp = await _context.Carrinhos.FindAsync(carrinhoId);
            if (carrinhoTemp is null) return NotFound();

            var produtoTemp = await _context.Produtos.FindAsync(produtoId);
            if (produtoTemp is null) return NotFound();

            carrinhoTemp.ItensCarrinho.Remove(produtoTemp);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}