using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeRestauranteApi.Data;
using LeRestauranteApi.Models;
using System.Linq;

namespace LeRestauranteApi.Controllers
{
    [ApiController]
    [Route("v1/fornecedores")]
    public class FornecedorController : ControllerBase
    {
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Fornecedor>> Delete([FromServices] DataContext context, int id)
        {
            var fornecedor = await context.Fornecedores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(fornecedor != null)
            {
                var produtos = await context.Produtos.Include(x => x.Fornecedor).AsNoTracking().Where(x => x.FornecedorId == id).ToListAsync();
                if (produtos != null && produtos.Count > 0 ) {
                    return BadRequest("Não é possível remover. Existem produtos relacionados ao fornecedor.");
                } else
                {
                    context.Fornecedores.Remove(fornecedor);
                    await context.SaveChangesAsync();
                }
            } else 
            {
                return NotFound();
            }
            return Ok("Fornecedor removido");
        }
        
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Fornecedor>>> Get([FromServices] DataContext context)
        {
            var fornecedores = await context.Fornecedores.ToListAsync();
            return fornecedores;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Fornecedor>> GetById([FromServices] DataContext context, int id)
        {
            var fornecedor = await context.Fornecedores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return fornecedor;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Fornecedor>> Post(
            [FromServices] DataContext context,
            [FromBody] Fornecedor model)
        {
            if (ModelState.IsValid)
            {
                context.Fornecedores.Add(model);
                await context.SaveChangesAsync();
                return model;
            } else 
            {
                return BadRequest(ModelState);
            }
        }
    }    
}