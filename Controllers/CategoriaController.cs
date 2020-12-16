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
            //carrega categoria por id
            var categoria = await context.Categorias
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == id);
            if(categoria != null)//se encontrou
            {
                //verifica se tem algum produto relacionado
                var produtos = await context.Produtos
                                    .AsNoTracking()
                                    .Where(x => x.CategoriaId == id)
                                    .ToListAsync();
                if (produtos != null && produtos.Count > 0 ) {//se tiver, emite mensagem
                    return BadRequest("Não é possível remover. Existem produtos relacionados à categoria.");
                } else
                {//se nao tiver, remove
                    context.Categorias.Remove(categoria);
                    await context.SaveChangesAsync();
                }
            } else 
            {//se nao encontrou retorna notFound
                return NotFound();
            }
            return Ok("Categoria removida");//se removeu retorna ok
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] DataContext context)
        {
            //lista todas as categorias
            var categorias = await context.Categorias.ToListAsync();
            return categorias;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> Post(
            [FromServices] DataContext context,
            [FromBody] Categoria model)
        {//cadastra categoria
            if (ModelState.IsValid)//se o model for valido
            {
                context.Categorias.Add(model);//faz um "insert"
                await context.SaveChangesAsync();//salva
                return model;//retorna categoria salva
            } else 
            {//se nao for valido retorna erro
                return BadRequest(ModelState);
            }
        }
    }    
}