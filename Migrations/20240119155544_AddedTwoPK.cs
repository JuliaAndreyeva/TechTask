using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonTree.Migrations
{
    /// <inheritdoc />
    public partial class AddedTwoPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectChilds",
                table: "ObjectChilds");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectChilds",
                table: "ObjectChilds",
                columns: new[] { "KeyId", "ChildId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectChilds",
                table: "ObjectChilds");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectChilds",
                table: "ObjectChilds",
                column: "KeyId");
        }
    }
}
