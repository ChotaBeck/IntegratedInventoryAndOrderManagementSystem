using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratedInventoryAndOrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class EditedpurchaseOrferItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceivedQuantity",
                table: "PurchaseOrderItems",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedQuantity",
                table: "PurchaseOrderItems");
        }
    }
}
