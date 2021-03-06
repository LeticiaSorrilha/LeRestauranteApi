
using System.ComponentModel.DataAnnotations;

namespace LeRestauranteApi.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Este campo é obrigatório.")]
        [MaxLength(60, ErrorMessage="Este campo deve conter entre 3 e 60 caracteres.")]
        [MinLength(3, ErrorMessage="Este campo deve conter entre 3 e 60 caracteres.")]
        public string Nome { get; set; }

        public decimal Pontuacao { get; set; }
        
    }
    
}