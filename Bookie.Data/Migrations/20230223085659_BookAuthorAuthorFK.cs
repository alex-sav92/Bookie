using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookie.DirectApp.Migrations
{
    /// <inheritdoc />
    public partial class BookAuthorAuthorFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "BookAuthor",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Author_AuthorId",
                table: "BookAuthor",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Author_AuthorId",
                table: "BookAuthor");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "BookAuthor");
        }
    }
}
