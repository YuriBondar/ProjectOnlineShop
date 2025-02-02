using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectEverythingForHomeOnlineShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedShopOrderStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShopOrderStatuses",
                columns: table => new
                {
                    ShopOrderStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopOrderStatusName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopOrderStatuses", x => x.ShopOrderStatusID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ShopOrderStatuses",
                columns: new[] { "ShopOrderStatusID", "ShopOrderStatusName" },
                values: new object[,]
                {
                    { 1, "Accepted" },
                    { 2, "Shipped" },
                    { 3, "Completted" },
                    { 100, "Canceled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrders_ShopOrderStatusID",
                table: "ShopOrders",
                column: "ShopOrderStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopOrders_ShopOrderStatuses_ShopOrderStatusID",
                table: "ShopOrders",
                column: "ShopOrderStatusID",
                principalTable: "ShopOrderStatuses",
                principalColumn: "ShopOrderStatusID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopOrders_ShopOrderStatuses_ShopOrderStatusID",
                table: "ShopOrders");

            migrationBuilder.DropTable(
                name: "ShopOrderStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ShopOrders_ShopOrderStatusID",
                table: "ShopOrders");
        }
    }
}
