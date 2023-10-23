using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Data;
using MinhaAPI.Models;

namespace MinhaAPI.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class ClienteController : ControllerBase
    {
        private LojaDbContext _dbContext;

        public ClienteController(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("Cadastrar_Cliente")]
        public async Task<ActionResult> Cadastrar_Cliente(Cliente cliente)
        {
            if (_dbContext is null) return NotFound();
            if (await _dbContext.Cliente.AnyAsync(c => c.ClienteId == cliente.ClienteId))
            {
                
                _dbContext.Cliente.Update(cliente);
            }
            else
            {
                
                await _dbContext.Cliente.AddAsync(cliente);
            }
            
            await _dbContext.SaveChangesAsync();
            return Created("", cliente);
        }

        [HttpGet]
        [Route("listar_Clientes")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Listar()
        {
            if (_dbContext is null) return NotFound();
            var clientes = await _dbContext.Cliente.ToListAsync();
            return clientes;
        }

        [HttpGet]
        [Route("buscar/{IdCliente}")]
        public async Task<ActionResult<Cliente>> Buscar(int IdCliente)
        {
            if (_dbContext is null) return NotFound();
            var clienteTemp = await _dbContext.Cliente.FindAsync(IdCliente);
            if (clienteTemp != null)
            {
                return clienteTemp;
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpPut()]
        [Route("alterar_cliente")]
        public async Task<ActionResult> Alterar_Cliente(Cliente cliente)
        {
            if (_dbContext is null) return NotFound();
            if (await _dbContext.Cliente.AnyAsync(c => c.ClienteId == cliente.ClienteId))
            {
                _dbContext.Cliente.Update(cliente);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound(); 
            }
        }
        
        [HttpPatch()]
        [Route("mudardarCPF/{idCliente}")]
        public async Task<ActionResult> MudarCPF(int idCliente, [FromForm] string cpfCliente)
        {
            if (_dbContext is null) return NotFound();
            var clienteTemp = await _dbContext.Cliente.FindAsync(idCliente);
            if (clienteTemp != null)
            {
                clienteTemp.CpfCliente = cpfCliente;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpDelete()]
        [Route("excluir_Cliente/{ID}")]
        public async Task<ActionResult> Excluir(int ID)
        {
            if (_dbContext is null) return NotFound();
            var clienteTemp = await _dbContext.Cliente.FindAsync(ID);
            if (clienteTemp != null)
            {
                _dbContext.Cliente.Remove(clienteTemp);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound(); 
            }
        }
    }
}
