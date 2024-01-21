using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonTree.Migrations
{
    /// <inheritdoc />
    public partial class DeletedPKNewTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectChilds",
                table: "ObjectChilds");

            migrationBuilder.DropIndex(
                name: "IX_ObjectChilds_ChildId",
                table: "ObjectChilds");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectChilds",
                table: "ObjectChilds",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectChilds_ParentId",
                table: "ObjectChilds",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectChilds",
                table: "ObjectChilds");

            migrationBuilder.DropIndex(
                name: "IX_ObjectChilds_ParentId",
                table: "ObjectChilds");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectChilds",
                table: "ObjectChilds",
                columns: new[] { "ParentId", "ChildId" });

            migrationBuilder.CreateIndex(
                name: "IX_ObjectChilds_ChildId",
                table: "ObjectChilds",
                column: "ChildId");
        }
    }
}
