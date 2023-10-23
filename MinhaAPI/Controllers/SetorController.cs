using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Data;
using MinhaAPI.Models;

namespace MinhaAPI.Controllers
{
    
    public class SetorController : ControllerBase
    {
        private LojaDbContext _dbContext;

        public SetorController(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("Cadastrar_Setor")]
        public async Task<ActionResult> Cadastrar_Categoria(Setor setor)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Cliente is null) return NotFound();
            await _dbContext.AddRangeAsync(setor);
            await _dbContext.SaveChangesAsync();
            return Created("", setor);
        }

        [HttpGet]
        [Route("listar_Setores")]
        public async Task<ActionResult<IEnumerable<Setor>>> Listar()
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Setor is null) return NotFound();
            return await _dbContext.Setor.ToListAsync();
        }
        [HttpGet]
        [Route("buscar/{Setor}")]
        public async Task<ActionResult<Setor>> Buscar(int setorId)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Categoria is null) return NotFound();
            var setorTemp = await _dbContext.Setor.FindAsync(setorId);
            if (setorTemp is null) return NotFound();
            return setorTemp;
        }

        [HttpPut()]
        [Route("alterarSetor")]
        public async Task<ActionResult> Alterar(Setor setor)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Setor is null) return NotFound();
            var setorTemp = await _dbContext.Setor.FindAsync(setor.IdSetor);
            if (setorTemp is null) return NotFound();
            _dbContext.Setor.Update(setor);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch()]
        [Route("mudardarDscricao/{nomeSetor}")]
        public async Task<ActionResult> MudarCPF(int setorId, [FromForm] string nomeSetor)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Setor is null) return NotFound();
            var setorTemp = await _dbContext.Setor.FindAsync(setorId);
            if (setorTemp is null) return NotFound();
            setorTemp.NomeSetor = nomeSetor;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete()]
        [Route("excluir_setor/{IDSetor}")]
        public async Task<ActionResult> Excluir(int ID)
        {

            if (_dbContext is null) return NotFound();
            if (_dbContext.Setor is null) return NotFound();
            var setorTemp = await _dbContext.Setor.FindAsync(ID);
            if (setorTemp is null) return NotFound();
            _dbContext.Remove(setorTemp);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }





    }
}
