using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.TotalErrorDbContext.Migrations
{
    public partial class Drop_columns_CountryId_from_Regions_table_and_SaleId_from_Orders_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SaleId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
