using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratedInventoryAndOrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a35c8897-e966-4cdf-8772-9b96e8defb24", null, "admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a35c8897-e966-4cdf-8772-9b96e8defb24");
        }
    }
}
