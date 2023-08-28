using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookVoyage.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_item_books_book_id",
                table: "order_item");

            migrationBuilder.DropIndex(
                name: "ix_order_item_book_id",
                table: "order_item");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "3203f2d8-8309-497d-8bbd-ef54fcbc3126");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "601660c9-f970-4c38-b00b-e4c32b399927");

            migrationBuilder.RenameColumn(
                name: "book_name",
                table: "order_item",
                newName: "book_ordered_item_book_name");

            migrationBuilder.RenameColumn(
                name: "book_id",
                table: "order_item",
                newName: "book_ordered_item_book_id");

            migrationBuilder.AddColumn<string>(
                name: "book_ordered_item_image_url",
                table: "order_item",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "8c389dbd-8ef0-4be3-8ca0-82977e6d8731", null, "customer", "CUSTOMER" },
                    { "c711e1e0-5279-44e4-bd36-c46f39089d8f", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "8c389dbd-8ef0-4be3-8ca0-82977e6d8731");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "c711e1e0-5279-44e4-bd36-c46f39089d8f");

            migrationBuilder.DropColumn(
                name: "book_ordered_item_image_url",
                table: "order_item");

            migrationBuilder.RenameColumn(
                name: "book_ordered_item_book_name",
                table: "order_item",
                newName: "book_name");

            migrationBuilder.RenameColumn(
                name: "book_ordered_item_book_id",
                table: "order_item",
                newName: "book_id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "3203f2d8-8309-497d-8bbd-ef54fcbc3126", null, "customer", "CUSTOMER" },
                    { "601660c9-f970-4c38-b00b-e4c32b399927", null, "admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_item_book_id",
                table: "order_item",
                column: "book_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_books_book_id",
                table: "order_item",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
