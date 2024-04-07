using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d41e5ea-c2ac-418e-98c2-90afd93fe598", "AQAAAAEAACcQAAAAEJfrtR07DYwuUP99o5tHWAO1BjdfaVJ63jKX2MMdqQr0LZVPcMillKTTwbOkEn2E9g==", "f6c0c43c-a6ad-4754-9f22-78440810a104" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db8b841e-824a-44ef-9ed4-4f708071ab7e", "AQAAAAEAACcQAAAAEEBsKeauewLkz/vHZgGgFsZ44AQRAOoVakj+nK+U1J5Z9O7WqAAm7xaxXDuGVFJOGA==", "27563151-9049-41aa-a609-b33b469fea6e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3c3284e-dfb0-4543-8046-bb77b2daeb29", "AQAAAAEAACcQAAAAEEXlgrwXIAV4M/rtArIo5BBOpVuwduK4p6TE6OlanagA0z2Jbxhyk0RY6KWb5e/Qng==", "73522ee7-77a6-4e05-9e3b-5f5f19d77763" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74204972-be2a-4dfb-a131-9a947545e1d7", "AQAAAAEAACcQAAAAEDDJ0Uw4J1WL0Y9Q7a9UOAF/xpTJPfthsSUej1Wf/wpeRslrsxhLW/B33KGWIAFi9w==", "3e2ddd43-a565-4a75-aba8-3f88e3068592" });
        }
    }
}
