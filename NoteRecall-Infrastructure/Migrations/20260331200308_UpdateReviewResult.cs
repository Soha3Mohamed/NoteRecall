using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteRecall_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReviewResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewResults_ReviewSessions_ReviewSessionId",
                table: "ReviewResults");

            migrationBuilder.DropColumn(
                name: "UserAnswer",
                table: "ReviewResults");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewSessionId",
                table: "ReviewResults",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewResults_ReviewSessions_ReviewSessionId",
                table: "ReviewResults",
                column: "ReviewSessionId",
                principalTable: "ReviewSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewResults_ReviewSessions_ReviewSessionId",
                table: "ReviewResults");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewSessionId",
                table: "ReviewResults",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserAnswer",
                table: "ReviewResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewResults_ReviewSessions_ReviewSessionId",
                table: "ReviewResults",
                column: "ReviewSessionId",
                principalTable: "ReviewSessions",
                principalColumn: "Id");
        }
    }
}
