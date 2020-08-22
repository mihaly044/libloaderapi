using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace libloaderapi.Migrations
{
    public partial class DataAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("241898df-227b-4c9b-87a1-7ff297e0a501"), new Guid("3ba84c73-5ed2-4789-8827-e8f730f42a82") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("8c01409d-0e8a-437a-8609-b3263bb36673"), new Guid("a43ea262-e4bd-463d-ad08-a38703469c47") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("e05e40e9-59ef-4a96-bb25-9f0b92d87d8d"), new Guid("ff663d9f-2fa8-4194-a936-4ad120667ca0") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3ba84c73-5ed2-4789-8827-e8f730f42a82"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a43ea262-e4bd-463d-ad08-a38703469c47"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ff663d9f-2fa8-4194-a936-4ad120667ca0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("241898df-227b-4c9b-87a1-7ff297e0a501"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c01409d-0e8a-437a-8609-b3263bb36673"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e05e40e9-59ef-4a96-bb25-9f0b92d87d8d"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sha1",
                table: "Binaries",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("714487dd-5cd0-4c89-8d5c-7e8c1a5c7c4e"), "LibAdmin" },
                    { new Guid("405c5b34-460d-4e24-a8bd-ae0bd142a861"), "LibUser" },
                    { new Guid("caba6a02-eecb-4b42-bebe-0012558c24a0"), "LibClient" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("0eb1f710-60f9-40f7-a2bc-aa00dea1551e"), "admin", "a94a8fe5ccb19ba61c4c0873d391e987982fbbd3" },
                    { new Guid("bbabf827-9051-45ae-ab8e-fba38b0d93f9"), "user", "a94a8fe5ccb19ba61c4c0873d391e987982fbbd3" },
                    { new Guid("7651e512-0331-45df-9613-338a652f6770"), "client", "a94a8fe5ccb19ba61c4c0873d391e987982fbbd3" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("0eb1f710-60f9-40f7-a2bc-aa00dea1551e"), new Guid("714487dd-5cd0-4c89-8d5c-7e8c1a5c7c4e") },
                    { new Guid("bbabf827-9051-45ae-ab8e-fba38b0d93f9"), new Guid("405c5b34-460d-4e24-a8bd-ae0bd142a861") },
                    { new Guid("7651e512-0331-45df-9613-338a652f6770"), new Guid("caba6a02-eecb-4b42-bebe-0012558c24a0") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("0eb1f710-60f9-40f7-a2bc-aa00dea1551e"), new Guid("714487dd-5cd0-4c89-8d5c-7e8c1a5c7c4e") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("7651e512-0331-45df-9613-338a652f6770"), new Guid("caba6a02-eecb-4b42-bebe-0012558c24a0") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("bbabf827-9051-45ae-ab8e-fba38b0d93f9"), new Guid("405c5b34-460d-4e24-a8bd-ae0bd142a861") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("405c5b34-460d-4e24-a8bd-ae0bd142a861"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("714487dd-5cd0-4c89-8d5c-7e8c1a5c7c4e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("caba6a02-eecb-4b42-bebe-0012558c24a0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0eb1f710-60f9-40f7-a2bc-aa00dea1551e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7651e512-0331-45df-9613-338a652f6770"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbabf827-9051-45ae-ab8e-fba38b0d93f9"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Sha1",
                table: "Binaries",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldMaxLength: 40);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3ba84c73-5ed2-4789-8827-e8f730f42a82"), "LibAdmin" },
                    { new Guid("a43ea262-e4bd-463d-ad08-a38703469c47"), "LibUser" },
                    { new Guid("ff663d9f-2fa8-4194-a936-4ad120667ca0"), "LibClient" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("241898df-227b-4c9b-87a1-7ff297e0a501"), "admin", "a94a8fe5ccb19ba61c4c0873d391e987982fbbd3" },
                    { new Guid("8c01409d-0e8a-437a-8609-b3263bb36673"), "user", "a94a8fe5ccb19ba61c4c0873d391e987982fbbd3" },
                    { new Guid("e05e40e9-59ef-4a96-bb25-9f0b92d87d8d"), "client", "a94a8fe5ccb19ba61c4c0873d391e987982fbbd3" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("241898df-227b-4c9b-87a1-7ff297e0a501"), new Guid("3ba84c73-5ed2-4789-8827-e8f730f42a82") },
                    { new Guid("8c01409d-0e8a-437a-8609-b3263bb36673"), new Guid("a43ea262-e4bd-463d-ad08-a38703469c47") },
                    { new Guid("e05e40e9-59ef-4a96-bb25-9f0b92d87d8d"), new Guid("ff663d9f-2fa8-4194-a936-4ad120667ca0") }
                });
        }
    }
}
