using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDV_Consultor.Migrations
{
    public partial class ClienteNovo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ClienteNovo",
                table: "Saidas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Troca",
                table: "Saidas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClienteNovo",
                table: "Saidas");

            migrationBuilder.DropColumn(
                name: "Troca",
                table: "Saidas");
        }
    }
}
