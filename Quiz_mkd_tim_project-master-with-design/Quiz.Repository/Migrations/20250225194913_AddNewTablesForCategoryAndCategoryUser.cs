using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTablesForCategoryAndCategoryUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaceOfOrigin",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Points = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Users_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Category_Users_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Users_CategoryId",
                table: "Category_Users",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Users_UserId",
                table: "Category_Users",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category_Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "PlaceOfOrigin",
                table: "AspNetUsers");
        }
    }
}
