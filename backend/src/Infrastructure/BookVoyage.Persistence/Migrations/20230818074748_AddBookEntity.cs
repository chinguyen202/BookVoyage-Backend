using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVoyage.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cart_items_books_book_id",
                table: "cart_items");

            migrationBuilder.DropForeignKey(
                name: "fk_cart_items_shopping_carts_shopping_cart_id",
                table: "cart_items");

            migrationBuilder.DropTable(
                name: "author_book");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cart_items",
                table: "cart_items");

            migrationBuilder.DropColumn(
                name: "client_secret",
                table: "shopping_carts");

            migrationBuilder.DropColumn(
                name: "stripe_payment_intent_id",
                table: "shopping_carts");

            migrationBuilder.RenameTable(
                name: "cart_items",
                newName: "cart_item");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "shopping_carts",
                newName: "buyer_id");

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
                name: "book_authors",
                columns: table => new
                {
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book_authors", x => new { x.book_id, x.author_id });
                    table.ForeignKey(
                        name: "fk_book_authors_authors_author_id",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_book_authors_books_book_id",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_book_authors_author_id",
                table: "book_authors",
                column: "author_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cart_item_books_book_id",
                table: "cart_item");

            migrationBuilder.DropForeignKey(
                name: "fk_cart_item_shopping_carts_shopping_cart_id",
                table: "cart_item");

            migrationBuilder.DropTable(
                name: "book_authors");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cart_item",
                table: "cart_item");

            migrationBuilder.RenameTable(
                name: "cart_item",
                newName: "cart_items");

            migrationBuilder.RenameColumn(
                name: "buyer_id",
                table: "shopping_carts",
                newName: "user_id");

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

            migrationBuilder.AddColumn<string>(
                name: "stripe_payment_intent_id",
                table: "shopping_carts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cart_items",
                table: "cart_items",
                column: "id");

            migrationBuilder.CreateTable(
                name: "author_book",
                columns: table => new
                {
                    authors_id = table.Column<Guid>(type: "uuid", nullable: false),
                    books_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_author_book", x => new { x.authors_id, x.books_id });
                    table.ForeignKey(
                        name: "fk_author_book_authors_authors_id",
                        column: x => x.authors_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_author_book_books_books_id",
                        column: x => x.books_id,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_author_book_books_id",
                table: "author_book",
                column: "books_id");

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
    }
}
