using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpellViewer.Migrations
{
    /// <inheritdoc />
    public partial class ChangingCharacters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masters_Spells_CharacterEntity_CharacterEntityId1",
                table: "Masters_Spells");

            migrationBuilder.DropIndex(
                name: "IX_Masters_Spells_CharacterEntityId1",
                table: "Masters_Spells");

            migrationBuilder.DropColumn(
                name: "CharacterEntityId1",
                table: "Masters_Spells");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterEntityId1",
                table: "Masters_Spells",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Masters_Spells_CharacterEntityId1",
                table: "Masters_Spells",
                column: "CharacterEntityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Masters_Spells_CharacterEntity_CharacterEntityId1",
                table: "Masters_Spells",
                column: "CharacterEntityId1",
                principalTable: "CharacterEntity",
                principalColumn: "Id");
        }
    }
}
