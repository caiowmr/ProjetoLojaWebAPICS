using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.Models
{
    public class Funcionario
    {
        [Key]
        public int IdFuncionario { get; set; }
        public string? NomeFuncionario { get; set; }
        public Setor setor { get; set; }

        
       

    }
}
