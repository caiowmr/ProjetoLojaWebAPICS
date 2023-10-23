using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Data;
using MinhaAPI.Models;

namespace MinhaAPI.Controllers
{
    [ApiController]
    [Route("api/categoria")] 
    
    public class CategoriaController : ControllerBase
    {
        
        private LojaDbContext _dbContext;
        
        public CategoriaController(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpPost]
        [Route("Cadastrar_Categoria")]
        public async Task<ActionResult> Cadastrar_Categoria(Categoria categoria)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Categoria is null) return NotFound();
            await _dbContext.Categoria.AddAsync(categoria); 
            await _dbContext.SaveChangesAsync();
            return Created("", categoria);
        }


        [HttpGet]
        [Route("listar_Categorias")]
        public async Task<ActionResult<IEnumerable<Categoria>>> Listar()
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Categoria is null) return NotFound();
            return await _dbContext.Categoria.ToListAsync();
        }
        [HttpGet]
        [Route("buscar/{Categoria}")]
        public async Task<ActionResult<Categoria>> Buscar(int categoriaId)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Categoria is null) return NotFound();
            var categoriaTemp = await _dbContext.Categoria.FindAsync(categoriaId);
            if (categoriaTemp is null) return NotFound();
            return categoriaTemp;
        }

        [HttpPut()]
        [Route("alterarCategoria")]
        public async Task<ActionResult> Alterar(Categoria categoria)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Categoria is null) return NotFound();
            var categoriaTemp = await _dbContext.Categoria.FindAsync(categoria.IdCategoria);
            if (categoriaTemp is null) return NotFound();
            _dbContext.Categoria.Update(categoria);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch()]
        [Route("mudardarNomeCategoria/{nomeCategoria}")]
        public async Task<ActionResult> MudarCategoria(int categoriaId, [FromForm] string NomeCategoria)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Categoria is null) return NotFound();
            var categoriaTemp = await _dbContext.Categoria.FindAsync(categoriaId);
            if (categoriaTemp is null) return NotFound();
            categoriaTemp.NomeCategoria = NomeCategoria;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete()]
        [Route("excluir_Categoria/{IDCategoria}")]
        public async Task<ActionResult> Excluir(int ID)
        {

            if (_dbContext is null) return NotFound();
            if (_dbContext.Categoria is null) return NotFound();
            var categoriaTemp = await _dbContext.Categoria.FindAsync(ID);
            if (categoriaTemp is null) return NotFound();
            _dbContext.Remove(categoriaTemp);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
