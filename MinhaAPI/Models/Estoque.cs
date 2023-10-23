using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.Models
{
    public class Estoque
    {
        [Key]
        public int IdEstoque { get; set; } 

        private List<Produto> _produtos;

        public Estoque()
        {
            _produtos = new List<Produto>();
        }

        public List<Produto> Produtos
        {
            get => _produtos;
            set => _produtos = value;
        }
    }
}