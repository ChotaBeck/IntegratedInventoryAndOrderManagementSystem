using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratedInventoryAndOrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class customerupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customers",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "LastName");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
