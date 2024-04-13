using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class UpdatedSubjectForStudentConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d60440cc-8ee4-4bf3-a481-c93cd83355b1", "AQAAAAEAACcQAAAAEEI04zp8dtviclO4NeQNSJH9iIgfF2FUxmk4kQ6kRO+4r1xhSRTAVdAKhyoO9+OQUg==", "3dc46513-2f4d-4fff-abbb-cc0c669a6c95" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "976bcb63-02d4-4aa7-872c-f6669d1f496a", "AQAAAAEAACcQAAAAEDnjgNd4vRvk7jJapwurNmj7NgbgbGI9G5fHa0Siu/uK2sHTaJO/Mfsd6eOrwHuyhg==", "5c0b3596-4ebc-42ce-ab36-1c6287a4a57a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
