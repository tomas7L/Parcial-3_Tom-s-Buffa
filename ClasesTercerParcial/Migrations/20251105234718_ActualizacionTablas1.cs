using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClasesTercerParcial.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionTablas1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Productos_ProductoId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_ProductoId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Productos");

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Ventas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductoVenta",
                columns: table => new
                {
                    ProductosId = table.Column<int>(type: "int", nullable: false),
                    VentasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoVenta", x => new { x.ProductosId, x.VentasId });
                    table.ForeignKey(
                        name: "FK_ProductoVenta_Productos_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoVenta_Ventas_VentasId",
                        column: x => x.VentasId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoVenta_VentasId",
                table: "ProductoVenta",
                column: "VentasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoVenta");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Productos");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Ventas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Ventas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ProductoId",
                table: "Ventas",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Productos_ProductoId",
                table: "Ventas",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
