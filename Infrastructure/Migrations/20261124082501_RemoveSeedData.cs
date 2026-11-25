using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "suppliers",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "suppliers",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "suppliers",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "suppliers",
                keyColumn: "id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "suppliers",
                columns: new[] { "id", "suppliername" },
                values: new object[,]
                {
                    { 1, "Asics" },
                    { 2, "Nike" },
                    { 3, "CLN Athletics" },
                    { 4, "Swix" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "description", "imageurl", "itemnumber", "productname", "price", "supplier_id" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet", "https://res.cloudinary.com/softtech-dev/image/upload/v1786556514/shoe-1_qig4ut.jpg", "1000", "Core 2000", 1295m, 1 },
                    { 2, "Lorem ipsum dolor sit amet", "https://res.cloudinary.com/softtech-dev/image/upload/v1786556530/shoe-3_qbzcwy.jpg", "1001", "Gel 27", 2295m, 2 }
                });
        }
    }
}
