
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LeRestauranteApi.Models
{
    [Keyless]//não precisa de chave - eu acho
    public class ItensDaVenda
    {        
        public int VendaId { get; set; }//relacao one to many com venda
        public Venda Venda { get; set; }
        public int  ProdutoId { get; set; }//relacao one to many com produto
        public Produto Produto { get; set; }
        public int quantidade { get; set; }
        
    }
    
}