using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca.Infrastructure.Migrations
{
    public partial class bbdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categories",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categories",
                table: "Books");
        }
    }
}