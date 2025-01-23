using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class eliminacionMenuPadreDeMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Menu_IdMenuPadre",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_IdMenuPadre",
                table: "Menu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Menu_IdMenuPadre",
                table: "Menu",
                column: "IdMenuPadre");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Menu_IdMenuPadre",
                table: "Menu",
                column: "IdMenuPadre",
                principalTable: "Menu",
                principalColumn: "Id");
        }
    }
}
