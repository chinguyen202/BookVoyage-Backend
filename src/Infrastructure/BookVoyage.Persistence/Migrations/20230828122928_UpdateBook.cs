using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookVoyage.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "32253ba3-20dc-4fb1-bfff-bd9797a87c4a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "dc40eb34-d4ad-463b-a564-a4aa169710dd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "3ee7e17a-9777-4cb1-95ff-d088ffcccb7c", null, "admin", "ADMIN" },
                    { "5075a6d8-2b24-45fa-b88b-40944c95b5dc", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "3ee7e17a-9777-4cb1-95ff-d088ffcccb7c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "5075a6d8-2b24-45fa-b88b-40944c95b5dc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "32253ba3-20dc-4fb1-bfff-bd9797a87c4a", "user", "customer", "CUSTOMER" },
                    { "dc40eb34-d4ad-463b-a564-a4aa169710dd", "a5e4b905-bf50-4877-9f09-ab4bc1ff62b3", "admin", "ADMIN" }
                });
        }
    }
}
