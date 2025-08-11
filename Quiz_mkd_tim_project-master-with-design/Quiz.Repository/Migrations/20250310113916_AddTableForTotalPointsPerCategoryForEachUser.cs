using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddTableForTotalPointsPerCategoryForEachUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTotalPointsPerCategories",
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
                    table.PrimaryKey("PK_UserTotalPointsPerCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTotalPointsPerCategories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTotalPointsPerCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTotalPointsPerCategories_CategoryId",
                table: "UserTotalPointsPerCategories",
                column: "CategoryId",
                unique: true,
                filter: "[CategoryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserTotalPointsPerCategories_UserId",
                table: "UserTotalPointsPerCategories",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTotalPointsPerCategories");
        }
    }
}
