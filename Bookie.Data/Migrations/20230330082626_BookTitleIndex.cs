using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookie.DirectApp.Migrations
{
    /// <inheritdoc />
    public partial class BookTitleIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "Book_Title",
                table: "Books",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Book_Title",
                table: "Books");
        }
    }
}
