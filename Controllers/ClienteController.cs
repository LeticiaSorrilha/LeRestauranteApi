using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeRestauranteApi.Data;
using LeRestauranteApi.Models;

namespace LeRestauranteApi.Controllers
{
    [ApiController]
    [Route("v1/clientes")]
    public class ClienteController : ControllerBase
    {
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Cliente>> Delete([FromServices] DataContext context, int id)
        {
            var cliente = await context.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(cliente != null)
            {
                context.Clientes.Remove(cliente);
                await context.SaveChangesAsync();
            } else 
            {
                return NotFound();
            }
            return Ok("Cliente removido");
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Cliente>>> Get([FromServices] DataContext context)
        {
            var clientes = await context.Clientes.ToListAsync();
            return clientes;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Cliente>> Post(
            [FromServices] DataContext context,
            [FromBody] Cliente model)
        {
            if (ModelState.IsValid)
            {
                context.Clientes.Add(model);
                await context.SaveChangesAsync();
                return model;
            } else 
            {
                return BadRequest(ModelState);
            }
        }
    }    
}