using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketplaceApp.Migrations
{
    /// <inheritdoc />
    public partial class dbInitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    registrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "quest",
                columns: table => new
                {
                    questId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    publishingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isSolved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quest", x => x.questId);
                    table.ForeignKey(
                        name: "FK_quest_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "solution",
                columns: table => new
                {
                    solutionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    questId = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sourceLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    isSelected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solution", x => x.solutionId);
                    table.ForeignKey(
                        name: "FK_solution_quest_questId",
                        column: x => x.questId,
                        principalTable: "quest",
                        principalColumn: "questId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_solution_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    commentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    solutionId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.commentId);
                    table.ForeignKey(
                        name: "FK_comment_solution_solutionId",
                        column: x => x.solutionId,
                        principalTable: "solution",
                        principalColumn: "solutionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comment_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comment_solutionId",
                table: "comment",
                column: "solutionId");

            migrationBuilder.CreateIndex(
                name: "IX_comment_userId",
                table: "comment",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_quest_userId",
                table: "quest",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_solution_questId",
                table: "solution",
                column: "questId");

            migrationBuilder.CreateIndex(
                name: "IX_solution_userId",
                table: "solution",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "solution");

            migrationBuilder.DropTable(
                name: "quest");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
