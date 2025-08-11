using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddedManyToManyRelationsToRangListAndUserTotalCategoryPointsPerCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RangList_TotalPointsPerCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RangListId = table.Column<int>(type: "int", nullable: true),
                    UserTotalPointsPerCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangList_TotalPointsPerCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RangList_TotalPointsPerCategories_RangLists_RangListId",
                        column: x => x.RangListId,
                        principalTable: "RangLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RangList_TotalPointsPerCategories_UserTotalPointsPerCategories_UserTotalPointsPerCategoryId",
                        column: x => x.UserTotalPointsPerCategoryId,
                        principalTable: "UserTotalPointsPerCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RangList_TotalPointsPerCategories_RangListId",
                table: "RangList_TotalPointsPerCategories",
                column: "RangListId");

            migrationBuilder.CreateIndex(
                name: "IX_RangList_TotalPointsPerCategories_UserTotalPointsPerCategoryId",
                table: "RangList_TotalPointsPerCategories",
                column: "UserTotalPointsPerCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RangList_TotalPointsPerCategories");
        }
    }
}
