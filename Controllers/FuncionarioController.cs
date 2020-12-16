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
    [Route("v1/funcionarios")]
    public class FuncionarioController : ControllerBase
    {
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Funcionario>> Delete([FromServices] DataContext context, int id)
        {
            var funcionario = await context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(funcionario != null)
            {
                context.Funcionarios.Remove(funcionario);
                await context.SaveChangesAsync();
            } else 
            {
                return NotFound();
            }
            return Ok("Funcionario removido");
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Funcionario>>> Get([FromServices] DataContext context)
        {
            var funcionarios = await context.Funcionarios.ToListAsync();
            return funcionarios;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Funcionario>> Post(
            [FromServices] DataContext context,
            [FromBody] Funcionario model)
        {
            if (ModelState.IsValid)
            {
                context.Funcionarios.Add(model);
                await context.SaveChangesAsync();
                return model;
            } else 
            {
                return BadRequest(ModelState);
            }
        }
    }    
}