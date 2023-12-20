using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDV_Consultor.Migrations
{
    public partial class SaidasPreco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "Saidas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Saidas");
        }
    }
}
