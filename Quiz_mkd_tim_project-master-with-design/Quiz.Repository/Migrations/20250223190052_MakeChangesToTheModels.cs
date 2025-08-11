using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Quiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class MakeChangesToTheModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Quizes_QuizId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_TypeQuizes_TypeQuizeId",
                table: "Quizes");

            migrationBuilder.DropTable(
                name: "TypeQuizes");

            migrationBuilder.DropIndex(
                name: "IX_Quizes_TypeQuizeId",
                table: "Quizes");

            migrationBuilder.DropIndex(
                name: "IX_Events_QuizId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TypeQuizeId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "TypeQuestionId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TypeQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeQuestions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Destination",
                value: "Битола");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Destination",
                value: "Скопје");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "Destination",
                value: "Куманово");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                column: "TypeQuestionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "TypeQuestionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                column: "TypeQuestionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                column: "TypeQuestionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                column: "TypeQuestionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                column: "TypeQuestionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                column: "TypeQuestionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                column: "TypeQuestionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                column: "TypeQuestionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                column: "TypeQuestionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11,
                column: "TypeQuestionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12,
                column: "TypeQuestionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 13,
                column: "TypeQuestionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 14,
                column: "TypeQuestionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 15,
                column: "TypeQuestionId",
                value: 1);

            migrationBuilder.InsertData(
                table: "TypeQuestions",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Географија" },
                    { 2, "Историја" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TypeQuestionId",
                table: "Questions",
                column: "TypeQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_TypeQuestions_TypeQuestionId",
                table: "Questions",
                column: "TypeQuestionId",
                principalTable: "TypeQuestions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_TypeQuestions_TypeQuestionId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "TypeQuestions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TypeQuestionId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TypeQuestionId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "TypeQuizeId",
                table: "Quizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeQuizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeQuizes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "QuizId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "QuizId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "QuizId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Quizes",
                keyColumn: "Id",
                keyValue: 1,
                column: "TypeQuizeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Quizes",
                keyColumn: "Id",
                keyValue: 2,
                column: "TypeQuizeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Quizes",
                keyColumn: "Id",
                keyValue: 3,
                column: "TypeQuizeId",
                value: 1);

            migrationBuilder.InsertData(
                table: "TypeQuizes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Географија" },
                    { 2, "Историја" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_TypeQuizeId",
                table: "Quizes",
                column: "TypeQuizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_QuizId",
                table: "Events",
                column: "QuizId",
                unique: true,
                filter: "[QuizId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Quizes_QuizId",
                table: "Events",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_TypeQuizes_TypeQuizeId",
                table: "Quizes",
                column: "TypeQuizeId",
                principalTable: "TypeQuizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
