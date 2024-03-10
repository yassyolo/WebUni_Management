using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class NormalizedUserNameForAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f0f561b-6afd-4702-8720-32c83f732a88", "AQAAAAEAACcQAAAAEF1haWyQ+iE4yu1DAHe9enJWtFycOVCU1BVPcpfWOSyKEvm42Fw49B+fp4VtygV3lg==", "9a4431dd-e6e3-4359-a689-283dc8de6b4b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8fe29ef0-1836-4bae-bcb9-d711a87a2808", "00000001", "AQAAAAEAACcQAAAAEOjP4JGlFifGx8T6GTwoq9ILsOfgN0R+NrIGA/wW0klS7oEJH4vFZ5KQt9E6RJ+B5w==", "66ca5005-90d9-4476-a7ef-5a47f5ee9247" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3dd014cc-8f52-4cb6-a813-e270619e8b1c", "AQAAAAEAACcQAAAAEL1A1DBggwytI10kS5qoFDHmKd3/WGjGTEkBOFjHr7IaS7NkhZGcObPr7SESj5Jttg==", "e032b298-7059-4535-809f-14beda39ad3f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3cb831fc-0c74-4a12-8849-3f4ce104a978", null, "AQAAAAEAACcQAAAAEHBzHw8an+oUnA7vffhdiHDnu6UXJt5fkDeHOacAHcYsEjYFrndEEKXy2Tt8MzMD6A==", "b6a6e389-561a-4167-b71e-2a6a54e106c5" });
        }
    }
}
