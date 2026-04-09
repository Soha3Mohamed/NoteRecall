using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteRecall_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionProgressRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_QuestionProgresses_QuestionId",
                table: "QuestionProgresses",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionProgresses_Questions_QuestionId",
                table: "QuestionProgresses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionProgresses_Questions_QuestionId",
                table: "QuestionProgresses");

            migrationBuilder.DropIndex(
                name: "IX_QuestionProgresses_QuestionId",
                table: "QuestionProgresses");
        }
    }
}
