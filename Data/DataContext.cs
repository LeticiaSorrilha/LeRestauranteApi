using Microsoft.EntityFrameworkCore;
using LeRestauranteApi.Models;

namespace LeRestauranteApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {            
        }
        public DbSet<Categoria> Categorias { get; set; }
         public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
         public DbSet<Funcionario> Funcionarios { get; set; }
         public DbSet<Cliente> Clientes { get; set; }
         public DbSet<Venda> Vendas { get; set; }
         public DbSet<ItensDaVenda> ItensDasVendas { get; set; }        
    }
    
}