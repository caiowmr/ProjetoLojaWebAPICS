using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.Models
{
    public class Carrinho
    {
        [Key]
        public int IdCarrinho { get; set; }
        public List<Produto> ItensCarrinho { get; set; } = new List<Produto>();
    }
}
