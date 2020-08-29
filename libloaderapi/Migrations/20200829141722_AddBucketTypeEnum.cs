using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace libloaderapi.Migrations
{
    public partial class AddBucketTypeEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e84358d-7850-432f-8879-4bd1dab2d3f3"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("698ed0ca-b489-4a8d-acaf-0d18c8a78d0c"), new Guid("c5f85b3a-cf5d-4d07-9456-3cd07fc26501") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("ccc10522-6109-47df-beed-08280777b5a9"), new Guid("a6442220-61c1-42b9-83b3-5602948d99be") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a6442220-61c1-42b9-83b3-5602948d99be"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c5f85b3a-cf5d-4d07-9456-3cd07fc26501"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("698ed0ca-b489-4a8d-acaf-0d18c8a78d0c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ccc10522-6109-47df-beed-08280777b5a9"));

            migrationBuilder.AddColumn<int>(
                name: "BucketType",
                table: "Clients",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "BucketType",
                table: "Clients");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a6442220-61c1-42b9-83b3-5602948d99be"), "LibAdmin" },
                    { new Guid("c5f85b3a-cf5d-4d07-9456-3cd07fc26501"), "LibUser" },
                    { new Guid("7e84358d-7850-432f-8879-4bd1dab2d3f3"), "LibClient" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("ccc10522-6109-47df-beed-08280777b5a9"), "admin", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" },
                    { new Guid("698ed0ca-b489-4a8d-acaf-0d18c8a78d0c"), "user", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("ccc10522-6109-47df-beed-08280777b5a9"), new Guid("a6442220-61c1-42b9-83b3-5602948d99be") },
                    { new Guid("698ed0ca-b489-4a8d-acaf-0d18c8a78d0c"), new Guid("c5f85b3a-cf5d-4d07-9456-3cd07fc26501") }
                });
        }
    }
}
