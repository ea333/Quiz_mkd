using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Quiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddedTablesForRangListAndRangListUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RangListId",
                table: "Category_Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RangLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RangLists_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RangList_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RangListId = table.Column<int>(type: "int", nullable: true),
                    Points = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangList_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RangList_Users_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RangList_Users_RangLists_RangListId",
                        column: x => x.RangListId,
                        principalTable: "RangLists",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "RangLists",
                columns: new[] { "Id", "EventId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Users_RangListId",
                table: "Category_Users",
                column: "RangListId");

            migrationBuilder.CreateIndex(
                name: "IX_RangList_Users_RangListId",
                table: "RangList_Users",
                column: "RangListId");

            migrationBuilder.CreateIndex(
                name: "IX_RangList_Users_UserId",
                table: "RangList_Users",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RangLists_EventId",
                table: "RangLists",
                column: "EventId",
                unique: true,
                filter: "[EventId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Users_RangLists_RangListId",
                table: "Category_Users",
                column: "RangListId",
                principalTable: "RangLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_RangLists_RangListId",
                table: "Category_Users");

            migrationBuilder.DropTable(
                name: "RangList_Users");

            migrationBuilder.DropTable(
                name: "RangLists");

            migrationBuilder.DropIndex(
                name: "IX_Category_Users_RangListId",
                table: "Category_Users");

            migrationBuilder.DropColumn(
                name: "RangListId",
                table: "Category_Users");
        }
    }
}
