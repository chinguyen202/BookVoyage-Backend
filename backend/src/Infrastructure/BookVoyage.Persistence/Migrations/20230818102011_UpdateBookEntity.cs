using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVoyage.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_books_categories_category_id",
                table: "books");

            migrationBuilder.DropTable(
                name: "book_authors");

            migrationBuilder.AddColumn<Guid>(
                name: "author_id",
                table: "books",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_books_author_id",
                table: "books",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "fk_books_authors_author_id",
                table: "books",
                column: "author_id",
                principalTable: "authors",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_books_categories_category_id",
                table: "books",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_books_authors_author_id",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "fk_books_categories_category_id",
                table: "books");

            migrationBuilder.DropIndex(
                name: "ix_books_author_id",
                table: "books");

            migrationBuilder.DropColumn(
                name: "author_id",
                table: "books");

            migrationBuilder.CreateTable(
                name: "book_authors",
                columns: table => new
                {
                    book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "fk_books_categories_category_id",
                table: "books",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
