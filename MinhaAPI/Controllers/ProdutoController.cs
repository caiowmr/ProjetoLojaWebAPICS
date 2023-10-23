using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Data;
using MinhaAPI.Models;

namespace MinhaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly LojaDbContext _context;

        public ProdutoController(LojaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Cadastrar_Produto")]
        public async Task<ActionResult> Cadastrar_Produto(Produto produto)
        {
            if (_context is null) return NotFound();

            if (await _context.Produtos.AnyAsync(p => p.IdProduto == produto.IdProduto))
            {
                _context.Produtos.Update(produto);
            }
            else
            {


                await _context.Produtos.AddAsync(produto);
            }

            await _context.SaveChangesAsync();
            return Created("", produto);
        }


        [HttpGet]
        [Route("listar_Produtos")]
        public async Task<ActionResult<IEnumerable<Produto>>> Listar()
        {
            if (_context is null) return NotFound();
            var produtos = await _context.Produtos.ToListAsync();
            return produtos;
        }

        [HttpGet]
        [Route("buscar/{produtoId}")]
        public async Task<ActionResult<Produto>> Buscar(int produtoId)
        {
            if (_context is null) return NotFound();
            var produtoTemp = await _context.Produtos.FindAsync(produtoId);
            if (produtoTemp != null)
            {
                return produtoTemp;
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpPut()]
        [Route("alterarProduto")]
        public async Task<ActionResult> Alterar(Produto produto)
        {
            if (_context is null) return NotFound();
            if (await _context.Produtos.AnyAsync(p => p.IdProduto == produto.IdProduto))
            {
                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpPatch()]
        [Route("mudardarNomeProduto/{produtoId}")]
        public async Task<ActionResult> MudarNomeProduto(int produtoId, [FromForm] string nomeProduto)
        {
            if (_context is null) return NotFound();
            if (await _context.Produtos.AnyAsync(p => p.IdProduto == produtoId))
            {
                var produtoTemp = await _context.Produtos.FindAsync(produtoId);
                if (produtoTemp != null)
                {
                    produtoTemp.NomeProduto = nomeProduto;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound(); 
                }
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpDelete()]
        [Route("excluir_Produto/{IDProduto}")]
        public async Task<ActionResult> Excluir(int IDProduto)
        {
            if (_context is null) return NotFound();
            if (await _context.Produtos.AnyAsync(p => p.IdProduto == IDProduto))
            {
                var produtoTemp = await _context.Produtos.FindAsync(IDProduto);
                if (produtoTemp != null)
                {
                    _context.Produtos.Remove(produtoTemp);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound(); 
                }
            }
            else
            {
                return NotFound(); 
            }
        }
    }
}
