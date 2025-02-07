using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NavigationsShippingAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ShippingAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_UserId",
                table: "ShippingAddresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_Users_UserId",
                table: "ShippingAddresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_Users_UserId",
                table: "ShippingAddresses");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAddresses_UserId",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShippingAddresses");
        }
    }
}
