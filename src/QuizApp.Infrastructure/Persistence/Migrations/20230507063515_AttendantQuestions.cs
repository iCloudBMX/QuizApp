using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AttendantQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamAttendantQuestion",
                columns: table => new
                {
                    ExamAttendantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAttendantQuestion", x => new { x.ExamAttendantId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_ExamAttendantQuestion_ExamAttendants_ExamAttendantId",
                        column: x => x.ExamAttendantId,
                        principalTable: "ExamAttendants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamAttendantQuestion_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttendantQuestion_QuestionId",
                table: "ExamAttendantQuestion",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamAttendantQuestion");
        }
    }
}
