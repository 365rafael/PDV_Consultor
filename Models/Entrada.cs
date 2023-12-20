using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDV_Consultor.Models
{
    public class Entrada
    {
        
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime DataEntrada { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoCompra { get; set; }
        public ProdutoItem ProdutoItem { get; set; }
    }
}
