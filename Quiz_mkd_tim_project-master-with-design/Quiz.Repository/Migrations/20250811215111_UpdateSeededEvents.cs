using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeededEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Провери си го знаење за Географија во Македонија", "Географија на Македонија" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Провери си го знаење за Историја во Македонија", "Историја на Македонија" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Провери си го знаење за Географија 2 дел во Македонија", "Географија на Македонија 2 дел" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Text",
                value: "Која е највисоката планина во Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Text",
                value: "Која е најголема река во Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Text",
                value: "Кое е најголемото природно езеро во Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Text",
                value: "Кој од наведените градови е втор по големина во Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                column: "Text",
                value: "Кое античко кралство ја опфаќало територијата на модерна Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                column: "Text",
                value: "Која година Македонија прогласи независност од Југославија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                column: "Text",
                value: "Која позната историска личност, родена во Пела, Грција, имаше значително влијание врз регионот на Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                column: "Text",
                value: "Која империја владеела со територијата на модерна Македонија над 500 години?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                column: "Text",
                value: "Како се викаше договорот со кој се реши долгогодишниот спор за името меѓу Грција и Македонија во 2018 година?");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Провери си го знаење за Географија во Северна Македонија", "Географија на Северна Македонија" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Провери си го знаење за Историја во Северна Македонија", "Историја на Северна Македонија" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Провери си го знаење за Географија 2 дел во Северна Македонија", "Географија на Северна Македонија 2 дел" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Text",
                value: "Која е највисоката планина во Северна Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Text",
                value: "Која е најголема река во Северна Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Text",
                value: "Кое е најголемото природно езеро во Северна Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Text",
                value: "Кој од наведените градови е втор по големина во Северна Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                column: "Text",
                value: "Кое античко кралство ја опфаќало територијата на модерна Северна Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                column: "Text",
                value: "Која година Северна Македонија прогласи независност од Југославија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                column: "Text",
                value: "Која позната историска личност, родена во Пела, Грција, имаше значително влијание врз регионот на Северна Македонија?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                column: "Text",
                value: "Која империја владеела со територијата на модерна Северна Македонија над 500 години?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                column: "Text",
                value: "Како се викаше договорот со кој се реши долгогодишниот спор за името меѓу Грција и Северна Македонија во 2018 година?");
        }
    }
}
