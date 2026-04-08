using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteRecall_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addNoteIdToReviewSessionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "ReviewSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "ReviewSessions");
        }
    }
}
