using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookVoyage.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_item_orders_order_id",
                table: "order_item");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "8c389dbd-8ef0-4be3-8ca0-82977e6d8731");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "c711e1e0-5279-44e4-bd36-c46f39089d8f");

            migrationBuilder.AlterColumn<Guid>(
                name: "order_id",
                table: "order_item",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "b0b54704-0fff-47f7-ab2f-c9bf9639a03f", null, "customer", "CUSTOMER" },
                    { "e35e4004-75b5-4c40-aa07-eb1cbececac4", null, "admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_orders_order_id",
                table: "order_item",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_item_orders_order_id",
                table: "order_item");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "b0b54704-0fff-47f7-ab2f-c9bf9639a03f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "e35e4004-75b5-4c40-aa07-eb1cbececac4");

            migrationBuilder.AlterColumn<Guid>(
                name: "order_id",
                table: "order_item",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "8c389dbd-8ef0-4be3-8ca0-82977e6d8731", null, "customer", "CUSTOMER" },
                    { "c711e1e0-5279-44e4-bd36-c46f39089d8f", null, "admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_orders_order_id",
                table: "order_item",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
