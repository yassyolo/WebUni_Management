using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class AddQrCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "QRCode",
                table: "Students",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04cf301b-b301-490a-b96f-aaf84857a256", "AQAAAAEAACcQAAAAEEyO9uYralcxq5fmoVsmjaEuZKARtxEYee6437GTd11EZb88d5UXMf87CywcPC17qQ==", "0907dde0-b18c-4c0a-b52a-5a1340ba2632" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a600a86-f4d5-47e8-90bf-ebc8fc3ddb4b", "AQAAAAEAACcQAAAAEDMTVDNOmzH0lE79AGugaGrBRVIVGwUnpW7juXwnWTGjEnv/9ho80Rq5RpV4G48VJQ==", "8417db81-0724-4ae7-8491-8a31092b9be0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRCode",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c393f2e6-5eef-4eff-bc4a-9cc9e4240b3e", "AQAAAAEAACcQAAAAEJ+AqJLGecGtS/diFau5t+uc5WYdmiTxQGcDi9ZXmpJ4j8aPqDTCrntB10TNhBDA/g==", "dc386f80-873d-4c8d-a7af-44b27c02061a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f984f945-9b89-4702-a9e7-a24ed8470d6a", "AQAAAAEAACcQAAAAEDK1eoZJYoHUhdUx3yxAtK+8oTN1Ogu4Ndqho45CMaLyMnAHTnhp7t03H4oDLHgFYw==", "ec9c5292-11c0-4aa0-b2e9-4f570e8987ad" });
        }
    }
}
