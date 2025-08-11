using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddedCascadeDeleteForEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_RangLists_RangListId",
                table: "Category_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_Events_EventId",
                table: "Events_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_RangList_Users_RangLists_RangListId",
                table: "RangList_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_RangLists_Events_EventId",
                table: "RangLists");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Users_RangLists_RangListId",
                table: "Category_Users",
                column: "RangListId",
                principalTable: "RangLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_Events_EventId",
                table: "Events_Users",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RangList_Users_RangLists_RangListId",
                table: "RangList_Users",
                column: "RangListId",
                principalTable: "RangLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RangLists_Events_EventId",
                table: "RangLists",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_RangLists_RangListId",
                table: "Category_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_Events_EventId",
                table: "Events_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_RangList_Users_RangLists_RangListId",
                table: "RangList_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_RangLists_Events_EventId",
                table: "RangLists");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Users_RangLists_RangListId",
                table: "Category_Users",
                column: "RangListId",
                principalTable: "RangLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_Events_EventId",
                table: "Events_Users",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RangList_Users_RangLists_RangListId",
                table: "RangList_Users",
                column: "RangListId",
                principalTable: "RangLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RangLists_Events_EventId",
                table: "RangLists",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
