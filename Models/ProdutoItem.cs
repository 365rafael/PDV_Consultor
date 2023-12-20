using System.ComponentModel.DataAnnotations;

namespace PDV_Consultor.Models
{
    public class ProdutoItem
    {
        [Key]
        public string SerialNumber { get; set; }
        public int ProdutoId { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Preco { get; set; }
        public ICollection<Saida> Saidas { get; set; }
        public virtual Produto Produto { get; set; }
        public bool Ativo { get; set; }

    }
}
