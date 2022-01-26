using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.TotalErrorDbContext.Migrations
{
    public partial class Remove_CreatedById_ModifiedById_and_DeletedById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "LastReadFiles");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "LastReadFiles");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "LastReadFiles");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Countries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "LastReadFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "LastReadFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "LastReadFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "ItemTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "ItemTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "ItemTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
