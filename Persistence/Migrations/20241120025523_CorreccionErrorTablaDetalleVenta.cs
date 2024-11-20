using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionErrorTablaDetalleVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleVenta",
                table: "DetalleVenta");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DetalleVenta",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleVenta",
                table: "DetalleVenta",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta_IdVenta",
                table: "DetalleVenta",
                column: "IdVenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleVenta",
                table: "DetalleVenta");

            migrationBuilder.DropIndex(
                name: "IX_DetalleVenta_IdVenta",
                table: "DetalleVenta");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DetalleVenta");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleVenta",
                table: "DetalleVenta",
                columns: new[] { "IdVenta", "IdProducto" });
        }
    }
}
