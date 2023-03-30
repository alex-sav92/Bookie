using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookie.DirectApp.Migrations
{
    /// <inheritdoc />
    public partial class BookAuthorUpdateKeyToComposite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthor_BookId",
                table: "BookAuthor");

            migrationBuilder.DropColumn(
                name: "BookAuthorId",
                table: "BookAuthor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                columns: new[] { "BookId", "AuthorId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor");

            migrationBuilder.AddColumn<int>(
                name: "BookAuthorId",
                table: "BookAuthor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                column: "BookAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_BookId",
                table: "BookAuthor",
                column: "BookId");
        }
    }
}
