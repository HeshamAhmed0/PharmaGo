using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Customer",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "Customer",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Customer",
                newName: "City");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Customer",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Customer",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Customer",
                newName: "Address_City");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Street",
                table: "Customer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Country",
                table: "Customer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address_City",
                table: "Customer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
