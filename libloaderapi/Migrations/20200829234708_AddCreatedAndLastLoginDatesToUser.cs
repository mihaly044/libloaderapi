using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace libloaderapi.Migrations
{
    public partial class AddCreatedAndLastLoginDatesToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eac855aa-41e8-4602-925e-d2156205badc"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("378ce141-8bca-41fa-989b-9d421dddd61d"), new Guid("9c6ef4b2-2d79-49fe-89fc-9c509dce77cb") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("cc75d881-42e4-41eb-b8f5-4e297e6e61b8"), new Guid("af6a4c69-5ad3-47b8-9208-e6a38ef78d18") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9c6ef4b2-2d79-49fe-89fc-9c509dce77cb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("af6a4c69-5ad3-47b8-9208-e6a38ef78d18"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("378ce141-8bca-41fa-989b-9d421dddd61d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cc75d881-42e4-41eb-b8f5-4e297e6e61b8"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "Users",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("00771ea4-a5d7-417c-bd14-b37146b0b1ed"), "LibAdmin" },
                    { new Guid("1d1ea70c-2999-403c-bb8a-185feee3d625"), "LibUser" },
                    { new Guid("9ecb0a15-7464-408a-8e9e-0791125eb91b"), "LibClient" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "LastLogin", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("03f09698-7c52-4657-b650-59b9f2af0d1d"), new DateTime(2020, 8, 29, 23, 47, 7, 664, DateTimeKind.Utc).AddTicks(1575), null, "admin", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" },
                    { new Guid("b5324445-d79a-457a-ab63-3a56d443a964"), new DateTime(2020, 8, 29, 23, 47, 7, 664, DateTimeKind.Utc).AddTicks(2566), null, "user", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("03f09698-7c52-4657-b650-59b9f2af0d1d"), new Guid("00771ea4-a5d7-417c-bd14-b37146b0b1ed") },
                    { new Guid("b5324445-d79a-457a-ab63-3a56d443a964"), new Guid("1d1ea70c-2999-403c-bb8a-185feee3d625") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9ecb0a15-7464-408a-8e9e-0791125eb91b"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("03f09698-7c52-4657-b650-59b9f2af0d1d"), new Guid("00771ea4-a5d7-417c-bd14-b37146b0b1ed") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("b5324445-d79a-457a-ab63-3a56d443a964"), new Guid("1d1ea70c-2999-403c-bb8a-185feee3d625") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00771ea4-a5d7-417c-bd14-b37146b0b1ed"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1d1ea70c-2999-403c-bb8a-185feee3d625"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("03f09698-7c52-4657-b650-59b9f2af0d1d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b5324445-d79a-457a-ab63-3a56d443a964"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9c6ef4b2-2d79-49fe-89fc-9c509dce77cb"), "LibAdmin" },
                    { new Guid("af6a4c69-5ad3-47b8-9208-e6a38ef78d18"), "LibUser" },
                    { new Guid("eac855aa-41e8-4602-925e-d2156205badc"), "LibClient" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("378ce141-8bca-41fa-989b-9d421dddd61d"), "admin", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" },
                    { new Guid("cc75d881-42e4-41eb-b8f5-4e297e6e61b8"), "user", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("378ce141-8bca-41fa-989b-9d421dddd61d"), new Guid("9c6ef4b2-2d79-49fe-89fc-9c509dce77cb") },
                    { new Guid("cc75d881-42e4-41eb-b8f5-4e297e6e61b8"), new Guid("af6a4c69-5ad3-47b8-9208-e6a38ef78d18") }
                });
        }
    }
}
