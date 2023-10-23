using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string? NomeCliente { get; set; }
        public string? CpfCliente { get; set; }


        public Cliente(int clienteId, string nomeCliente, string cpfCliente)
        {
            ClienteId = clienteId;
            NomeCliente = nomeCliente;
            CpfCliente = cpfCliente;
        }


    }
}
