using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string? NomeCategoria { get; set; }
        

    }
}
