using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDV_Consultor.Models
{
    public class Cliente
    {
        
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
