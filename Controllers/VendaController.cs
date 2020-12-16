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
    [Route("v1/vendas")]
    public class VendaController : ControllerBase
    {
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> Delete([FromServices] DataContext context, int id)
        {
            var venda = await context.Vendas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(venda != null)
            {
                var itens = await context.ItensDasVendas.Include(x => x.Venda).AsNoTracking().Where(x => x.VendaId == id).ToListAsync();
                if (itens != null && itens.Count > 0 ) {
                    itens.ForEach(item => context.ItensDasVendas.Remove(item));
                    await context.SaveChangesAsync();
                }
                context.Vendas.Remove(venda);
                await context.SaveChangesAsync();
            } else 
            {
                return NotFound();
            }
            return Ok("Categoria removida");
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Venda>>> Get([FromServices] DataContext context)
        {
            var vendas = await context.Vendas.ToListAsync();
            return vendas;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Venda>> Post(
            [FromServices] DataContext context,
            [FromBody] Venda model)
        {
            if (ModelState.IsValid)
            {
                context.Vendas.Add(model);
                await context.SaveChangesAsync();
                return model;
            } else 
            {
                return BadRequest(ModelState);
            }
        }
    }    
}