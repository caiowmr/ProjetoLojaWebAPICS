using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.Models
{
    public class Setor
    {
        [Key]
        public int IdSetor { get; set; }    
        public string? NomeSetor { get; set; }   
        
    }
}
