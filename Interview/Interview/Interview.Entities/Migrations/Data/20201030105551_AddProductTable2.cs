using Microsoft.EntityFrameworkCore.Migrations;

namespace Interview.Entities.Migrations.Data
{
    public partial class AddProductTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "Products");
        }
    }
}
