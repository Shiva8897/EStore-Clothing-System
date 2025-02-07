using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wislistproductIndbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistProduct_Products_ProductId",
                table: "WishlistProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistProduct_WishList_WishListId",
                table: "WishlistProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistProduct",
                table: "WishlistProduct");

            migrationBuilder.RenameTable(
                name: "WishlistProduct",
                newName: "WishlistProducts");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistProduct_WishListId",
                table: "WishlistProducts",
                newName: "IX_WishlistProducts_WishListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishlistProducts",
                table: "WishlistProducts",
                columns: new[] { "ProductId", "WishListId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistProducts_Products_ProductId",
                table: "WishlistProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistProducts_WishList_WishListId",
                table: "WishlistProducts",
                column: "WishListId",
                principalTable: "WishList",
                principalColumn: "WishListId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistProducts_Products_ProductId",
                table: "WishlistProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistProducts_WishList_WishListId",
                table: "WishlistProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistProducts",
                table: "WishlistProducts");

            migrationBuilder.RenameTable(
                name: "WishlistProducts",
                newName: "WishlistProduct");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistProducts_WishListId",
                table: "WishlistProduct",
                newName: "IX_WishlistProduct_WishListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishlistProduct",
                table: "WishlistProduct",
                columns: new[] { "ProductId", "WishListId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistProduct_Products_ProductId",
                table: "WishlistProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistProduct_WishList_WishListId",
                table: "WishlistProduct",
                column: "WishListId",
                principalTable: "WishList",
                principalColumn: "WishListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
