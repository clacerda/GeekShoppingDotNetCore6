using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekShopping.CouponApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouponDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "Coupon_Code", "Discount_Amount" },
                values: new object[,]
                {
                    { 1L, "Claudio_2024", 10m },
                    { 2L, "Claudio_2025", 15m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
