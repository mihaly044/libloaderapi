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
                    Name = table.Column<string>(nullable: true)
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
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Binaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sha1 = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Binaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Binaries_Users_UserId",
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
                    { new Guid("2c3b97d9-dfd1-4466-8a57-2a3b1eff9a82"), "LibAdmin" },
                    { new Guid("92c2517d-6f2f-4a93-9345-0293a65e0936"), "LibUser" },
                    { new Guid("3b82c301-dea3-4054-8a22-475b86261384"), "LibClient" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("d8f4e084-8dc2-4af6-8d7a-c596e8b87d4f"), "admin", "nimda" },
                    { new Guid("a310e717-cc64-45dd-922e-1974d2fd5a80"), "user", "resu" },
                    { new Guid("785fb01c-2e8f-40f5-8b69-ec082842447f"), "client", "tneilc" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("d8f4e084-8dc2-4af6-8d7a-c596e8b87d4f"), new Guid("2c3b97d9-dfd1-4466-8a57-2a3b1eff9a82") },
                    { new Guid("a310e717-cc64-45dd-922e-1974d2fd5a80"), new Guid("92c2517d-6f2f-4a93-9345-0293a65e0936") },
                    { new Guid("785fb01c-2e8f-40f5-8b69-ec082842447f"), new Guid("3b82c301-dea3-4054-8a22-475b86261384") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Binaries_UserId",
                table: "Binaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Binaries");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
