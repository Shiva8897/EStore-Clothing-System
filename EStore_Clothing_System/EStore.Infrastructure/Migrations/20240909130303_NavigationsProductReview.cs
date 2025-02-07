using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NavigationsProductReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductVariantId",
                table: "ProductReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProductReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductVariantId",
                table: "ProductReviews",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_UserId",
                table: "ProductReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_ProductVariants_ProductVariantId",
                table: "ProductReviews",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Users_UserId",
                table: "ProductReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_ProductVariants_ProductVariantId",
                table: "ProductReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Users_UserId",
                table: "ProductReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_ProductVariantId",
                table: "ProductReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_UserId",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "ProductVariantId",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductReviews");
        }
    }
}
