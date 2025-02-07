using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wishlistrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishlistProducts");

            migrationBuilder.AddColumn<int>(
                name: "WishListId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_WishListId",
                table: "Products",
                column: "WishListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_WishList_WishListId",
                table: "Products",
                column: "WishListId",
                principalTable: "WishList",
                principalColumn: "WishListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_WishList_WishListId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_WishListId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WishListId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "WishlistProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WishListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistProducts", x => new { x.ProductId, x.WishListId });
                    table.ForeignKey(
                        name: "FK_WishlistProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistProducts_WishList_WishListId",
                        column: x => x.WishListId,
                        principalTable: "WishList",
                        principalColumn: "WishListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishlistProducts_WishListId",
                table: "WishlistProducts",
                column: "WishListId");
        }
    }
}
