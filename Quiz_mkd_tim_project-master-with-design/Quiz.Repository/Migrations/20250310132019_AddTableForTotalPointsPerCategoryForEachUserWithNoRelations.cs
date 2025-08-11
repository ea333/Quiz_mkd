using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddTableForTotalPointsPerCategoryForEachUserWithNoRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTotalPointsPerCategories_AspNetUsers_UserId",
                table: "UserTotalPointsPerCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTotalPointsPerCategories_Categories_CategoryId",
                table: "UserTotalPointsPerCategories");

            migrationBuilder.DropIndex(
                name: "IX_UserTotalPointsPerCategories_CategoryId",
                table: "UserTotalPointsPerCategories");

            migrationBuilder.DropIndex(
                name: "IX_UserTotalPointsPerCategories_UserId",
                table: "UserTotalPointsPerCategories");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTotalPointsPerCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTotalPointsPerCategories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserTotalPointsPerCategories_AspNetUsers_UserId",
                table: "UserTotalPointsPerCategories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTotalPointsPerCategories_Categories_CategoryId",
                table: "UserTotalPointsPerCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
