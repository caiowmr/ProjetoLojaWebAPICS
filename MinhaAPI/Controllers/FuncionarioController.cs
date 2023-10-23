using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Data;
using MinhaAPI.Models;

namespace MinhaAPI.Controllers
{
    public class FuncionarioController : ControllerBase
    {
        private LojaDbContext _dbContext;

        public FuncionarioController(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        [Route("Cadastrar_Funcionario")]
        public async Task<ActionResult> Cadastrar_Funcionario(Funcionario funcionario)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Cliente is null) return NotFound();
            await _dbContext.AddRangeAsync(funcionario);
            await _dbContext.SaveChangesAsync();
            return Created("", funcionario);
        }

        [HttpGet]
        [Route("listar_Funcionarios")]
        public async Task<ActionResult<IEnumerable<Funcionario>>> Listar()
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Funcionario is null) return NotFound();
            return await _dbContext.Funcionario.ToListAsync();
        }
        [HttpGet]
        [Route("buscar/{Funcionarioa}")]
        public async Task<ActionResult<Funcionario>> Buscar(int funcionarioId)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Funcionario is null) return NotFound();
            var funcionarioTemp = await _dbContext.Funcionario.FindAsync(funcionarioId);
            if (funcionarioTemp is null) return NotFound();
            return funcionarioTemp;
        }
        

        [HttpPut()]
        [Route("alterarFuncionario")]
        public async Task<ActionResult> Alterar(Funcionario funcionario)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Funcionario is null) return NotFound();
            var funcionarioTemp = await _dbContext.Funcionario.FindAsync(funcionario.IdFuncionario);
            if (funcionarioTemp is null) return NotFound();
            _dbContext.Funcionario.Update(funcionario);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch()]
        [Route("mudardarDscricao/{idFuncionario}")]
        public async Task<ActionResult> MudarCPF(int funcionarioId, [FromForm] string funcionario)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Funcionario is null) return NotFound();
            var funcionarioTemp = await _dbContext.Funcionario.FindAsync(funcionarioId);
            if (funcionarioTemp is null) return NotFound();
            funcionarioTemp.NomeFuncionario = funcionario;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete()]
        [Route("Demitir_Funcionario/{IDFuncionario}")]
        public async Task<ActionResult> Excluir(int ID)
        {

            if (_dbContext is null) return NotFound();
            if (_dbContext.Funcionario is null) return NotFound();
            var funcionarioTemp = await _dbContext.Funcionario.FindAsync(ID);
            if (funcionarioTemp is null) return NotFound();
            _dbContext.Remove(funcionarioTemp);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

   
    }
}