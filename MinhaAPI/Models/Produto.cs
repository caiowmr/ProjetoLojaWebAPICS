using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.Models
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }
        public string? NomeProduto { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
            public int CategoriaId { get; set; } 
            
        
    }
}

