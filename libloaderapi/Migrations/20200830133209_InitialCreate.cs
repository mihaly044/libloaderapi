using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace libloaderapi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    Password = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sha256 = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    Key = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUsed = table.Column<DateTime>(nullable: true),
                    RegistrantIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    BucketType = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("cbe24cd4-ff5c-46bf-a439-925d27801fd0"), "LibAdmin" },
                    { new Guid("2f484d7d-4dee-43a6-879c-f52f7eaef704"), "LibUser" },
                    { new Guid("f3c4187f-802d-4246-ae57-5cba7de0f40c"), "LibClient" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "LastLogin", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("4c7f78f3-0672-4677-9906-fdef5ff356ec"), new DateTime(2020, 8, 30, 13, 32, 9, 497, DateTimeKind.Utc).AddTicks(1592), null, "admin", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" },
                    { new Guid("364c3f87-9c5c-49be-a33e-7941701d5ed5"), new DateTime(2020, 8, 30, 13, 32, 9, 497, DateTimeKind.Utc).AddTicks(2481), null, "user", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("4c7f78f3-0672-4677-9906-fdef5ff356ec"), new Guid("cbe24cd4-ff5c-46bf-a439-925d27801fd0") },
                    { new Guid("364c3f87-9c5c-49be-a33e-7941701d5ed5"), new Guid("2f484d7d-4dee-43a6-879c-f52f7eaef704") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
