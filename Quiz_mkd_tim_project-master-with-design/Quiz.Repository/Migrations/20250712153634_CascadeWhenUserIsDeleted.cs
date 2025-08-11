using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CascadeWhenUserIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_AspNetUsers_UserId",
                table: "Category_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_EventPending_Users_AspNetUsers_UserId",
                table: "EventPending_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_AspNetUsers_UserId",
                table: "Events_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_RangList_Users_AspNetUsers_UserId",
                table: "RangList_Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Users_AspNetUsers_UserId",
                table: "Category_Users",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventPending_Users_AspNetUsers_UserId",
                table: "EventPending_Users",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_AspNetUsers_UserId",
                table: "Events_Users",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RangList_Users_AspNetUsers_UserId",
                table: "RangList_Users",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_AspNetUsers_UserId",
                table: "Category_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_EventPending_Users_AspNetUsers_UserId",
                table: "EventPending_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_AspNetUsers_UserId",
                table: "Events_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_RangList_Users_AspNetUsers_UserId",
                table: "RangList_Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Users_AspNetUsers_UserId",
                table: "Category_Users",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventPending_Users_AspNetUsers_UserId",
                table: "EventPending_Users",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_AspNetUsers_UserId",
                table: "Events_Users",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RangList_Users_AspNetUsers_UserId",
                table: "RangList_Users",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
