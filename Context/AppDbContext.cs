using Microsoft.EntityFrameworkCore;
//using PDV_Consultor.Controllers;
using PDV_Consultor.Models;

namespace PDV_Consultor.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Saida> Saidas { get; set; }
        public DbSet<ProdutoItem> ProdutoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais do modelo, como chaves primárias compostas, relacionamentos, etc.
        }

        public DbSet<PDV_Consultor.Models.ProdutoItem>? ProdutoItem { get; set; }
    }
}
