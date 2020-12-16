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
    [Route("v1/categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> Delete([FromServices] DataContext context, int id)
        {
            var categoria = await context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(categoria != null)
            {
                var produtos = await context.Produtos.Include(x => x.Categoria).AsNoTracking().Where(x => x.FornecedorId == id).ToListAsync();
                if (produtos != null && produtos.Count > 0 ) {
                    return BadRequest("Não é possível remover. Existem produtos relacionados à categoria.");
                } else
                {
                    context.Categorias.Remove(categoria);
                    await context.SaveChangesAsync();
                }
            } else 
            {
                return NotFound();
            }
            return Ok("Categoria removida");
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] DataContext context)
        {
            var categorias = await context.Categorias.ToListAsync();
            return categorias;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> Post(
            [FromServices] DataContext context,
            [FromBody] Categoria model)
        {
            if (ModelState.IsValid)
            {
                context.Categorias.Add(model);
                await context.SaveChangesAsync();
                return model;
            } else 
            {
                return BadRequest(ModelState);
            }
        }
    }    
}