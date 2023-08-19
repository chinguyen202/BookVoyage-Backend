using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVoyage.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_secret",
                table: "shopping_carts");

            migrationBuilder.DropColumn(
                name: "stripe_payment_intended",
                table: "shopping_carts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "client_secret",
                table: "shopping_carts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "stripe_payment_intended",
                table: "shopping_carts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
