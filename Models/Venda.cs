using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeRestauranteApi.Models
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }//relacao one to many com cliente
        public Cliente Cliente { get; set; }
        public decimal TotalVenda { get; set; }
       
    }
    
}