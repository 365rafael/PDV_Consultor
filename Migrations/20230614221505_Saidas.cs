using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDV_Consultor.Migrations
{
    public partial class Saidas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Saidas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Saidas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
