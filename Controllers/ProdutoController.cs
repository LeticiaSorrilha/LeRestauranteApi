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
    [Route("v1/produtos")]
    public class ProdutoController : ControllerBase
    {
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Produto>> Delete([FromServices] DataContext context, int id)
        {
            var produto = await context.Produtos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(produto != null)
            {
                context.Produtos.Remove(produto);
                await context.SaveChangesAsync();
            } else 
            {
                return NotFound();
            }
            return Ok("Produto removido");
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Produto>>> Get([FromServices] DataContext context)
        {
            var produtos = await context.Produtos
                            .Include(x => x.Categoria)
                            .Include(x => x.Fornecedor)
                            .ToListAsync();
            return produtos;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Produto>> Post(
            [FromServices] DataContext context,
            [FromBody] Produto model)
        {
            if (ModelState.IsValid)
            {
                context.Produtos.Add(model);
                await context.SaveChangesAsync();
                return await GetById(context,model.Id);
            } else 
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Produto>> GetById([FromServices] DataContext context, int id)
        {
            var produto = await context.Produtos
                                .Include(x => x.Fornecedor)
                                .Include(x => x.Categoria)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);
            return produto;
        }

        [HttpGet]
        [Route("categorias/{id:int}")]
        public async Task<ActionResult<List<Produto>>> GetByCategoria([FromServices] DataContext context, int id)
        {
            var produtos = await context.Produtos
                                .Include(x => x.Fornecedor)
                                .Include(x => x.Categoria)
                                .AsNoTracking()
                                .Where(x => x.CategoriaId == id)
                                .ToListAsync();
            return produtos;
        }

        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<ActionResult<Produto>> Update(
            int id, 
            [FromServices] DataContext context,
            [FromBody] Produto model
        )
        {
 
            var produtoToUpdate = await context.Produtos
                .FirstOrDefaultAsync(x => x.Id == id);
            
            produtoToUpdate.Nome = model.Nome;
            produtoToUpdate.Preco = model.Preco;
            produtoToUpdate.FornecedorId = model.FornecedorId;
 
            Fornecedor fornecedor = await context.Fornecedores.SingleAsync(a => a.Id == model.FornecedorId);
            produtoToUpdate.Fornecedor = fornecedor;
            
            produtoToUpdate.QtdEmEstoque = model.QtdEmEstoque;
            produtoToUpdate.QtdMinima = model.QtdMinima;
            await context.SaveChangesAsync();
 
            return produtoToUpdate;
        }
    }    
}