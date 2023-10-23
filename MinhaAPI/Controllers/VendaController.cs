using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Data;
using MinhaAPI.Models;

namespace MinhaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly LojaDbContext _context;

        public VendaController(LojaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Cadastrar_Venda")]
        public async Task<ActionResult> Cadastrar_Venda([FromBody] Venda venda)
        {
            if (_context is null) return NotFound();
            if (_context.Venda is null) return NotFound();

            // Busca o carrinho do cliente
            var carrinho = await _context.Carrinhos.FindAsync(venda.CarrinhoId);
            if (carrinho is null) return NotFound();

            // Calcula o valor total da venda
            var valorTotal = carrinho.ItensCarrinho.Sum(item => item.Preco * item.Quantidade);

            // Cria uma nova venda
            venda = new Venda(venda.ClienteId, venda.CarrinhoId);
            venda.ValorTotal = valorTotal;

            // Adiciona a venda ao banco de dados
            await _context.Venda.AddAsync(venda);
            await _context.SaveChangesAsync();

            // Retorna a venda criada
            return Created("", venda);
        }

        [HttpGet]
        [Route("listar_Vendas")]
        public async Task<ActionResult<IEnumerable<Venda>>> Listar()
        {
            if (_context is null) return NotFound();
            if (_context.Venda is null) return NotFound();
            return await _context.Venda.ToListAsync();
        }

        [HttpGet]
        [Route("buscar/{venda}")]
        public async Task<ActionResult<Venda>> Buscar(int vendaId)
        {
            if (_context is null) return NotFound();
            if (_context.Venda is null) return NotFound();
            var vendaTemp = await _context.Venda.FindAsync(vendaId);
            if (vendaTemp is null) return NotFound();
            return vendaTemp;
        }
    }
}
