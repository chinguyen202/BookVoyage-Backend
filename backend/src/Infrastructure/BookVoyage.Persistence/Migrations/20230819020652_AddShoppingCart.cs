using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVoyage.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_books_categories_category_id",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "fk_cart_item_books_book_id",
                table: "cart_item");

            migrationBuilder.DropForeignKey(
                name: "fk_cart_item_shopping_carts_shopping_cart_id",
                table: "cart_item");

            migrationBuilder.DropTable(
                name: "order_details");

            migrationBuilder.DropTable(
                name: "order_headers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cart_item",
                table: "cart_item");

            migrationBuilder.RenameTable(
                name: "cart_item",
                newName: "cart_items");

            migrationBuilder.RenameIndex(
                name: "ix_cart_item_shopping_cart_id",
                table: "cart_items",
                newName: "ix_cart_items_shopping_cart_id");

            migrationBuilder.RenameIndex(
                name: "ix_cart_item_book_id",
                table: "cart_items",
                newName: "ix_cart_items_book_id");

            migrationBuilder.AddColumn<string>(
                name: "client_secret",
                table: "shopping_carts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "shopping_carts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_at",
                table: "shopping_carts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "stripe_payment_intended",
                table: "shopping_carts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cart_items",
                table: "cart_items",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_books_categories_category_id",
                table: "books",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cart_items_books_book_id",
                table: "cart_items",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cart_items_shopping_carts_shopping_cart_id",
                table: "cart_items",
                column: "shopping_cart_id",
                principalTable: "shopping_carts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_books_categories_category_id",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "fk_cart_items_books_book_id",
                table: "cart_items");

            migrationBuilder.DropForeignKey(
                name: "fk_cart_items_shopping_carts_shopping_cart_id",
                table: "cart_items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cart_items",
                table: "cart_items");

            migrationBuilder.DropColumn(
                name: "client_secret",
                table: "shopping_carts");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "shopping_carts");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "shopping_carts");

            migrationBuilder.DropColumn(
                name: "stripe_payment_intended",
                table: "shopping_carts");

            migrationBuilder.RenameTable(
                name: "cart_items",
                newName: "cart_item");

            migrationBuilder.RenameIndex(
                name: "ix_cart_items_shopping_cart_id",
                table: "cart_item",
                newName: "ix_cart_item_shopping_cart_id");

            migrationBuilder.RenameIndex(
                name: "ix_cart_items_book_id",
                table: "cart_item",
                newName: "ix_cart_item_book_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cart_item",
                table: "cart_item",
                column: "id");

            migrationBuilder.CreateTable(
                name: "order_headers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    order_total = table.Column<double>(type: "double precision", nullable: false),
                    order_total_quantity = table.Column<int>(type: "integer", nullable: false),
                    pickup_email = table.Column<string>(type: "text", nullable: false),
                    pickup_name = table.Column<string>(type: "text", nullable: false),
                    pickup_phone_number = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    stripe_payment_intent_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_headers", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_headers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    item_name = table.Column<string>(type: "text", nullable: false),
                    order_header_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_details_books_book_id",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_details_order_headers_order_header_id",
                        column: x => x.order_header_id,
                        principalTable: "order_headers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_details_book_id",
                table: "order_details",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_details_order_header_id",
                table: "order_details",
                column: "order_header_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_headers_user_id",
                table: "order_headers",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_books_categories_category_id",
                table: "books",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cart_item_books_book_id",
                table: "cart_item",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cart_item_shopping_carts_shopping_cart_id",
                table: "cart_item",
                column: "shopping_cart_id",
                principalTable: "shopping_carts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
