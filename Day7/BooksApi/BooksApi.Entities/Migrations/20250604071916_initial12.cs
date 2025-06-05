using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksApi.Entities.Migrations
{
    /// <inheritdoc />
    public partial class initial12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDetails_Users_UserId",
                table: "BookDetails");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "BookDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_UserId1",
                table: "BookDetails",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookDetails_Users_UserId",
                table: "BookDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookDetails_Users_UserId1",
                table: "BookDetails",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDetails_Users_UserId",
                table: "BookDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookDetails_Users_UserId1",
                table: "BookDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookDetails_UserId1",
                table: "BookDetails");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BookDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_BookDetails_Users_UserId",
                table: "BookDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
