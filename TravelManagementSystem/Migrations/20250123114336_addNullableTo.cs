using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class addNullableTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesTables_Agents_AgentId",
                table: "SalesTables");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesTables_Customers_CustomerId",
                table: "SalesTables");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "SalesTables",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "SalesTables",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTables_Agents_AgentId",
                table: "SalesTables",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTables_Customers_CustomerId",
                table: "SalesTables",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesTables_Agents_AgentId",
                table: "SalesTables");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesTables_Customers_CustomerId",
                table: "SalesTables");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "SalesTables",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "SalesTables",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTables_Agents_AgentId",
                table: "SalesTables",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTables_Customers_CustomerId",
                table: "SalesTables",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
