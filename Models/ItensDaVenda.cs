
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LeRestauranteApi.Models
{
    [Keyless]
    public class ItensDaVenda
    {        
        public int VendaId { get; set; }
        public Venda Venda { get; set; }
        public int  ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int quantidade { get; set; }
        
    }
    
}