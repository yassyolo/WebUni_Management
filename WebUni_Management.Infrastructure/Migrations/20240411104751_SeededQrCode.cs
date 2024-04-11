using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeededQrCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74545a5b-7312-4bf3-bc10-91b1f25fe611", "AQAAAAEAACcQAAAAEMjqIZjSkX7QTVGTRPaIk9KYXVpQFelsBtcTAMDSlQh2s9YJu2C28hpzTi2UBvsNeQ==", "cffd9a6e-c55e-4bd2-8190-e4364bd27aaf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5513968-c5f8-4f76-85cf-bdb10da08f95", "AQAAAAEAACcQAAAAEASMMpfvRgUZC+ysYecpxv8EgILhbjkRzNCRxym/5Pr64HsDwKRzbgiqrSAxiY36XA==", "1d7101d2-46fe-4503-b49b-d8ea8763ef32" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "QRCode",
                value: new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 2, 68, 0, 0, 2, 68, 1, 0, 0, 0, 0, 223, 155, 6, 247, 0, 0, 1, 239, 73, 68, 65, 84, 120, 156, 237, 220, 107, 14, 130, 48, 16, 4, 96, 110, 192, 253, 111, 201, 13, 52, 18, 77, 97, 217, 106, 140, 143, 46, 201, 215, 31, 38, 66, 25, 147, 201, 204, 108, 187, 8, 211, 229, 75, 99, 153, 32, 225, 137, 10, 184, 69, 22, 200, 76, 21, 65, 229, 180, 46, 176, 126, 178, 58, 180, 138, 182, 71, 176, 151, 178, 83, 180, 163, 214, 47, 208, 87, 209, 53, 210, 93, 211, 59, 212, 99, 61, 99, 7, 121, 138, 99, 190, 29, 123, 124, 116, 167, 64, 194, 19, 21, 112, 139, 44, 40, 147, 153, 237, 120, 248, 186, 108, 129, 123, 83, 32, 225, 137, 10, 184, 69, 22, 20, 200, 204, 80, 12, 86, 144, 251, 71, 119, 10, 36, 60, 81, 1, 183, 200, 130, 194, 153, 185, 158, 8, 11, 125, 72, 120, 162, 2, 110, 145, 5, 103, 201, 204, 252, 24, 36, 60, 81, 1, 183, 200, 130, 178, 153, 153, 1, 127, 165, 215, 3, 9, 79, 84, 192, 45, 178, 224, 247, 153, 25, 70, 40, 11, 115, 62, 5, 18, 158, 168, 128, 91, 100, 65, 145, 204, 204, 199, 163, 24, 188, 158, 7, 9, 79, 84, 192, 45, 178, 96, 116, 102, 174, 87, 237, 70, 59, 145, 1, 247, 43, 2, 36, 60, 81, 1, 183, 200, 130, 97, 153, 217, 185, 89, 186, 197, 108, 191, 8, 9, 79, 84, 192, 45, 178, 160, 88, 102, 134, 235, 183, 181, 97, 119, 246, 117, 69, 128, 132, 39, 42, 224, 22, 89, 240, 231, 204, 12, 127, 107, 217, 174, 231, 179, 162, 241, 164, 34, 64, 194, 19, 21, 112, 139, 44, 24, 157, 153, 161, 67, 147, 213, 129, 240, 59, 144, 240, 68, 5, 220, 34, 11, 70, 103, 102, 54, 2, 112, 222, 100, 135, 132, 39, 42, 224, 22, 89, 80, 34, 51, 179, 171, 194, 165, 161, 74, 64, 194, 19, 21, 112, 139, 44, 168, 149, 153, 217, 19, 69, 217, 189, 211, 236, 1, 35, 72, 120, 162, 2, 110, 145, 5, 37, 50, 51, 20, 131, 123, 9, 104, 255, 91, 220, 189, 128, 235, 105, 69, 128, 132, 39, 42, 224, 22, 89, 80, 34, 51, 67, 75, 102, 247, 14, 69, 72, 120, 162, 2, 110, 145, 5, 39, 201, 204, 236, 53, 184, 144, 240, 68, 5, 220, 34, 11, 42, 102, 230, 1, 248, 208, 136, 153, 242, 158, 59, 36, 60, 81, 1, 183, 200, 130, 241, 153, 121, 188, 99, 218, 158, 2, 125, 175, 34, 64, 194, 19, 21, 112, 139, 44, 248, 127, 102, 126, 54, 32, 225, 137, 10, 184, 69, 22, 200, 76, 21, 65, 229, 180, 46, 176, 126, 178, 58, 180, 138, 182, 71, 176, 151, 178, 83, 180, 163, 214, 47, 208, 87, 209, 53, 210, 93, 211, 59, 212, 99, 61, 81, 7, 249, 10, 132, 214, 64, 118, 252, 131, 247, 221, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c8da985-7e57-42e0-865c-a95d9e4858d2", "AQAAAAEAACcQAAAAEFPXP9akaUxMOLiOuLHpdUY8TEcyTxXE8VjaNfFNsRro3kvYVvBL8rUQul8SGk6OEw==", "49e3d0f3-b632-40d4-b5d4-80dde4c52460" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af2bc163-4b8e-4949-bb13-a2c8f5db61a1", "AQAAAAEAACcQAAAAEGFAV3NAqcqQlAcLcw5z9GHxXib2VCtKi1s5C2RxNel6PASCqF+D+oN6YZzZGP3TSQ==", "17d9c542-e67a-4cb5-b6e0-91b8d0467366" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "QRCode",
                value: null);
        }
    }
}
