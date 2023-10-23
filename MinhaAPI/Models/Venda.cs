using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaAPI.Models
{
    public class Venda
    {
        [Key]
        public int IdVenda { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey("Carrinho")]
        public int CarrinhoId { get; set; }
        public Carrinho Carrinho { get; set; }


        public DateTime DataVenda { get; set; }
        public double ValorTotal { get; set; }

        public Venda() { }

        public Venda(int clienteId, int carrinhoId)
        {
            ClienteId = clienteId;
            CarrinhoId = carrinhoId;
            DataVenda = DateTime.Now;
        }
    }
}
