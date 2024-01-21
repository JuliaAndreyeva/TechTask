using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonTree.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjectChilds_ObjectModels_KeyId",
                table: "ObjectChilds");

            migrationBuilder.RenameColumn(
                name: "KeyId",
                table: "ObjectChilds",
                newName: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectChilds_ObjectModels_ParentId",
                table: "ObjectChilds",
                column: "ParentId",
                principalTable: "ObjectModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjectChilds_ObjectModels_ParentId",
                table: "ObjectChilds");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "ObjectChilds",
                newName: "KeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectChilds_ObjectModels_KeyId",
                table: "ObjectChilds",
                column: "KeyId",
                principalTable: "ObjectModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
