using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpellViewer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpellComponentsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Is_material = table.Column<bool>(type: "bit", nullable: false),
                    Is_somatic = table.Column<bool>(type: "bit", nullable: false),
                    Is_verbal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellComponentsEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    MoreData = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpellMaterialsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Materials_needed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpellComponentsEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellMaterialsEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpellMaterialsEntity_SpellComponentsEntity_SpellComponentsEntityId",
                        column: x => x.SpellComponentsEntityId,
                        principalTable: "SpellComponentsEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacterEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterRace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Masters_Spells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Casting_time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentsId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Range = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is_ritual = table.Column<bool>(type: "bit", nullable: false),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterEntityId = table.Column<int>(type: "int", nullable: true),
                    CharacterEntityId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters_Spells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Masters_Spells_CharacterEntity_CharacterEntityId",
                        column: x => x.CharacterEntityId,
                        principalTable: "CharacterEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Masters_Spells_CharacterEntity_CharacterEntityId1",
                        column: x => x.CharacterEntityId1,
                        principalTable: "CharacterEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Masters_Spells_SpellComponentsEntity_ComponentsId",
                        column: x => x.ComponentsId,
                        principalTable: "SpellComponentsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpellClassesEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpellId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellClassesEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpellClassesEntity_Masters_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Masters_Spells",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEntity_UserId",
                table: "CharacterEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Masters_Spells_CharacterEntityId",
                table: "Masters_Spells",
                column: "CharacterEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Masters_Spells_CharacterEntityId1",
                table: "Masters_Spells",
                column: "CharacterEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Masters_Spells_ComponentsId",
                table: "Masters_Spells",
                column: "ComponentsId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleEntity_UserId",
                table: "RoleEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellClassesEntity_SpellId",
                table: "SpellClassesEntity",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellMaterialsEntity_SpellComponentsEntityId",
                table: "SpellMaterialsEntity",
                column: "SpellComponentsEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleEntity");

            migrationBuilder.DropTable(
                name: "SpellClassesEntity");

            migrationBuilder.DropTable(
                name: "SpellMaterialsEntity");

            migrationBuilder.DropTable(
                name: "Masters_Spells");

            migrationBuilder.DropTable(
                name: "CharacterEntity");

            migrationBuilder.DropTable(
                name: "SpellComponentsEntity");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
