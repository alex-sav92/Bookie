using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookie.DirectApp.Migrations
{
    /// <inheritdoc />
    public partial class indexComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Review_Comment",
                table: "Review",
                column: "Comment",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Review_Comment",
                table: "Review");
        }
    }
}
