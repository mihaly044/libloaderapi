using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace libloaderapi.Migrations
{
    public partial class SHA1SamplePasswords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("785fb01c-2e8f-40f5-8b69-ec082842447f"), new Guid("3b82c301-dea3-4054-8a22-475b86261384") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("a310e717-cc64-45dd-922e-1974d2fd5a80"), new Guid("92c2517d-6f2f-4a93-9345-0293a65e0936") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("d8f4e084-8dc2-4af6-8d7a-c596e8b87d4f"), new Guid("2c3b97d9-dfd1-4466-8a57-2a3b1eff9a82") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2c3b97d9-dfd1-4466-8a57-2a3b1eff9a82"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3b82c301-dea3-4054-8a22-475b86261384"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("92c2517d-6f2f-4a93-9345-0293a65e0936"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("785fb01c-2e8f-40f5-8b69-ec082842447f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a310e717-cc64-45dd-922e-1974d2fd5a80"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d8f4e084-8dc2-4af6-8d7a-c596e8b87d4f"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
