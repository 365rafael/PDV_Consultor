using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDV_Consultor.Models
{
    public class Saida
    {
        
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataSaida { get; set; }
        public string NomeCliente { get; set; }
        public bool ClienteNovo { get; set; }
        public bool Troca { get; set; }
        public decimal Preco { get; set; }
        public virtual ProdutoItem ProdutoItem { get; set; }
    }
}
