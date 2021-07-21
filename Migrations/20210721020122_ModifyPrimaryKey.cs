using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNET_Core_API_1.Migrations
{
    public partial class ModifyPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductInvoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductInvoice",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
