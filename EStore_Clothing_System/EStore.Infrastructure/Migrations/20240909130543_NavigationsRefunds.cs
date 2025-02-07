using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NavigationsRefunds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Refunds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_OrderItemId",
                table: "Refunds",
                column: "OrderItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Refunds_OrderItems_OrderItemId",
                table: "Refunds",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "OrderItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refunds_OrderItems_OrderItemId",
                table: "Refunds");

            migrationBuilder.DropIndex(
                name: "IX_Refunds_OrderItemId",
                table: "Refunds");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Refunds");
        }
    }
}
