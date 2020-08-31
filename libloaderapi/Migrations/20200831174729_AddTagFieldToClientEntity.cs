using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace libloaderapi.Migrations
{
    public partial class AddTagFieldToClientEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f3c4187f-802d-4246-ae57-5cba7de0f40c"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("364c3f87-9c5c-49be-a33e-7941701d5ed5"), new Guid("2f484d7d-4dee-43a6-879c-f52f7eaef704") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("4c7f78f3-0672-4677-9906-fdef5ff356ec"), new Guid("cbe24cd4-ff5c-46bf-a439-925d27801fd0") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2f484d7d-4dee-43a6-879c-f52f7eaef704"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cbe24cd4-ff5c-46bf-a439-925d27801fd0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("364c3f87-9c5c-49be-a33e-7941701d5ed5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4c7f78f3-0672-4677-9906-fdef5ff356ec"));

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Clients",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("811e7e43-2d39-40ca-8ea3-586d37be13f9"), "LibAdmin" },
                    { new Guid("25729831-4801-4455-aee4-b3b06fc71e1d"), "LibUser" },
                    { new Guid("9089d067-61a3-4f1f-a7f0-43c083e4913b"), "LibClient" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "LastLogin", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("421a0420-734f-4c91-b199-b2ceebc245fe"), new DateTime(2020, 8, 31, 17, 47, 29, 35, DateTimeKind.Utc).AddTicks(4039), null, "admin", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" },
                    { new Guid("664b1e24-ed92-4b5a-88ef-40a87f44d9a8"), new DateTime(2020, 8, 31, 17, 47, 29, 35, DateTimeKind.Utc).AddTicks(4947), null, "user", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("421a0420-734f-4c91-b199-b2ceebc245fe"), new Guid("811e7e43-2d39-40ca-8ea3-586d37be13f9") },
                    { new Guid("664b1e24-ed92-4b5a-88ef-40a87f44d9a8"), new Guid("25729831-4801-4455-aee4-b3b06fc71e1d") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9089d067-61a3-4f1f-a7f0-43c083e4913b"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("421a0420-734f-4c91-b199-b2ceebc245fe"), new Guid("811e7e43-2d39-40ca-8ea3-586d37be13f9") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("664b1e24-ed92-4b5a-88ef-40a87f44d9a8"), new Guid("25729831-4801-4455-aee4-b3b06fc71e1d") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("25729831-4801-4455-aee4-b3b06fc71e1d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("811e7e43-2d39-40ca-8ea3-586d37be13f9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("421a0420-734f-4c91-b199-b2ceebc245fe"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("664b1e24-ed92-4b5a-88ef-40a87f44d9a8"));

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Clients");

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
        }
    }
}
