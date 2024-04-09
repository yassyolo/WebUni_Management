using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedFacultyAndMajorDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Major",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                defaultValue: "",
                comment: "Major description");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Faculty",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                defaultValue: "",
                comment: "Faculty description");

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

            migrationBuilder.UpdateData(
                table: "Faculty",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "The Mathematics Faculty offers a wide range of courses in pure and applied mathematics, statistics, and computer science. Our faculty members are dedicated to providing students with a solid foundation in mathematical theory and practical skills, preparing them for successful careers in academia, industry, and research.");

            migrationBuilder.UpdateData(
                table: "Major",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "The Computer Science major equips students with the knowledge and skills needed to excel in the rapidly evolving field of technology. Our comprehensive curriculum covers programming languages, algorithms, data structures, software engineering, and more, preparing students for diverse career opportunities in software development, cybersecurity, artificial intelligence, and beyond.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Major");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Faculty");

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
    }
}
