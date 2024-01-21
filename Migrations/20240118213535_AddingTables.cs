using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonTree.Migrations
{
    /// <inheritdoc />
    public partial class AddingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ObjectModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRoot = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectChilds",
                columns: table => new
                {
                    KeyId = table.Column<int>(type: "int", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectChilds", x => x.KeyId);
                    table.ForeignKey(
                        name: "FK_ObjectChilds_ObjectModels_ChildId",
                        column: x => x.ChildId,
                        principalTable: "ObjectModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObjectChilds_ObjectModels_KeyId",
                        column: x => x.KeyId,
                        principalTable: "ObjectModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObjectChilds_ChildId",
                table: "ObjectChilds",
                column: "ChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjectChilds");

            migrationBuilder.DropTable(
                name: "ObjectModels");
        }
    }
}
