using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Quiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddedTheCategoriesToDbAndManyToManyRelationsWithCategoryAndRangList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category_RangLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    RangListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_RangLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_RangLists_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Category_RangLists_RangLists_RangListId",
                        column: x => x.RangListId,
                        principalTable: "RangLists",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "categoryName" },
                values: new object[,]
                {
                    { 1, "Ж.Стил" },
                    { 2, "Спорт" },
                    { 3, "Наука" },
                    { 4, "Култура" },
                    { 5, "Медиуми" },
                    { 6, "Свет" },
                    { 7, "Историја" },
                    { 8, "Забава" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_RangLists_CategoryId",
                table: "Category_RangLists",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_RangLists_RangListId",
                table: "Category_RangLists",
                column: "RangListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category_RangLists");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
