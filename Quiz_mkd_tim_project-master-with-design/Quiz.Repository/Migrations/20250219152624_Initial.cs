using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Quiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<double>(type: "float", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeQuizeId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizes_TypeQuizes_TypeQuizeId",
                        column: x => x.TypeQuizeId,
                        principalTable: "TypeQuizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Quizes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Quizes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_Users_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EndDate", "ImageUrl", "Name", "QuizId", "StartDate" },
                values: new object[] { 3, "Провери си го знаење за Географија 2 дел во Северна Македонија", new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Географија на Северна Македонија 2 дел", null, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TypeQuizes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Географија" },
                    { 2, "Историја" }
                });

            migrationBuilder.InsertData(
                table: "Quizes",
                columns: new[] { "Id", "FileName", "ImageUrl", "Name", "TypeQuizeId" },
                values: new object[,]
                {
                    { 1, "", "", "Брза Географија", 1 },
                    { 2, "", "", "Пат низ минатотo", 2 },
                    { 3, "", "", "Брза географија 2 дел", 1 }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EndDate", "ImageUrl", "Name", "QuizId", "StartDate" },
                values: new object[,]
                {
                    { 1, "Провери си го знаење за Географија во Северна Македонија", new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Географија на Северна Македонија", 1, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Провери си го знаење за Историја во Северна Македонија", new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Историја на Северна Македонија", 2, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "QuizId", "Text" },
                values: new object[,]
                {
                    { 1, 1, "Која е највисоката планина во Северна Македонија?" },
                    { 2, 1, "Која е најголема река во Северна Македонија?" },
                    { 3, 1, "Кое е најголемото природно езеро во Северна Македонија?" },
                    { 4, 1, "Кој национален парк е дом на загрозениот балкански рис?" },
                    { 5, 1, "Кој од наведените градови е втор по големина во Северна Македонија?" },
                    { 6, 2, "Кое античко кралство ја опфаќало територијата на модерна Северна Македонија?" },
                    { 7, 2, "Која година Северна Македонија прогласи независност од Југославија?" },
                    { 8, 2, "Која позната историска личност, родена во Пела, Грција, имаше значително влијание врз регионот на Северна Македонија?" },
                    { 9, 2, "Која империја владеела со територијата на модерна Северна Македонија над 500 години?" },
                    { 10, 2, "Како се викаше договорот со кој се реши долгогодишниот спор за името меѓу Грција и Северна Македонија во 2018 година?" },
                    { 11, 3, "Кој е најголемиот остров во Охридското Езеро?" },
                    { 12, 3, "Која карпеста формација во Македонија е позната како „Камени кукли“ ?" },
                    { 13, 3, "Кој град во Македонија се наоѓа најсеверно?" },
                    { 14, 3, "Која е најдлабоката пештера во Македонија?" },
                    { 15, 3, "На која река се наоѓа Козјак – најголемото вештачко езеро во Македонија?" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "Text", "isCorrect" },
                values: new object[,]
                {
                    { 1, 1, "Јакупица", false },
                    { 2, 1, "Шар Планина", false },
                    { 3, 1, "Кораб", true },
                    { 4, 1, "Пелистер", false },
                    { 5, 2, "Треска", false },
                    { 6, 2, "Црна Река", false },
                    { 7, 2, "Брегалница", false },
                    { 8, 2, "Вардар", true },
                    { 9, 3, "Преспа", false },
                    { 10, 3, "Дојран", false },
                    { 11, 3, "Охрид", true },
                    { 12, 3, "Тиквеш", false },
                    { 13, 4, "Галичица", false },
                    { 14, 4, "Маврово", true },
                    { 15, 4, "Јасен", false },
                    { 16, 4, "Пелистер", false },
                    { 17, 5, "Битола", true },
                    { 18, 5, "Охрид", false },
                    { 19, 5, "Куманово", false },
                    { 20, 5, "Тетово", false },
                    { 21, 6, "Дарданиа", false },
                    { 22, 6, "Пеониа", true },
                    { 23, 6, "Алирија", false },
                    { 24, 6, "Англија", false },
                    { 25, 7, "1989", false },
                    { 26, 7, "1995", false },
                    { 27, 7, "1991", true },
                    { 28, 7, "2001", false },
                    { 29, 8, "Филип II Македонски", false },
                    { 30, 8, "Александар Македонски", true },
                    { 31, 8, "Јустинијан I", false },
                    { 32, 8, "Скендербег", false },
                    { 33, 9, "Римската империја", false },
                    { 34, 9, "Византиска империја", false },
                    { 35, 9, "Отоманската империја", true },
                    { 36, 9, "Австро - унгарската империја", false },
                    { 37, 10, "Скопски договор", false },
                    { 38, 10, "Договорот од Преспа", true },
                    { 39, 10, "Балкански договор", false },
                    { 40, 10, "Охридски рамковен договор", false },
                    { 41, 11, "Градско Острово", false },
                    { 42, 11, "Голем Град", true },
                    { 43, 11, "Мал Град", false },
                    { 44, 11, "Пештани", false },
                    { 45, 12, "Куклица", true },
                    { 46, 12, "Маркови Кули", false },
                    { 47, 12, "Долни Полог", false },
                    { 48, 12, "Плочата", false },
                    { 49, 13, "Куманово", true },
                    { 50, 13, "Крива Паланка", false },
                    { 51, 13, "Тетово", false },
                    { 52, 13, "Кратово", false },
                    { 53, 14, "Слатински Извор", false },
                    { 54, 14, "Врело", true },
                    { 55, 14, "Алилица", false },
                    { 56, 14, "Голубарница", false },
                    { 57, 15, "Треска", true },
                    { 58, 15, "Вардар", false },
                    { 59, 15, "Радика", false },
                    { 60, 15, "Црна", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_QuizId",
                table: "Events",
                column: "QuizId",
                unique: true,
                filter: "[QuizId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Users_EventId",
                table: "Events_Users",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Users_UserId",
                table: "Events_Users",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_TypeQuizeId",
                table: "Quizes",
                column: "TypeQuizeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Events_Users");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Quizes");

            migrationBuilder.DropTable(
                name: "TypeQuizes");
        }
    }
}
