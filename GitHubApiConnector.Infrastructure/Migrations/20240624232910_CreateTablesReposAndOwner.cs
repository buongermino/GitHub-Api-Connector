using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitHubApiConnector.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesReposAndOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GitHubRepoOwners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GitHubUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtmlUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReposUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationsUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GitHubRepoOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GitHubRepos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GitHubId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GitUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SshUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloneUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Watchers = table.Column<int>(type: "int", nullable: false),
                    Forks = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtmlUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PushedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GitHubRepoOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GitHubRepos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GitHubRepos_GitHubRepoOwners_GitHubRepoOwnerId",
                        column: x => x.GitHubRepoOwnerId,
                        principalTable: "GitHubRepoOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GitHubRepos_GitHubRepoOwnerId",
                table: "GitHubRepos",
                column: "GitHubRepoOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GitHubRepos");

            migrationBuilder.DropTable(
                name: "GitHubRepoOwners");
        }
    }
}
