using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class IsNewsArticleApproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "NewsArticles",
                type: "bit",
                nullable: true,
                comment: "Is article approved for publishing");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d51ddd1d-ea53-4637-89cf-680841d35354", "AQAAAAEAACcQAAAAEOCOZxobV+1YNm7soqtCQ6bKGKrlqPQTB/p2ZdATelPQS71BGaPPIjsioHgQPgnQBA==", "7cd26622-8795-432f-8ef8-a73f21d16848" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bb0da00-51a7-403a-a381-e3bf0348bba8", "AQAAAAEAACcQAAAAEI6Zjx9/X7hmB/uHoUXGZKUNdhuI25FQU9cBPWngJ/TV7w1aLstcdZl85d/+VpBXoQ==", "1b9f75bb-f25c-4db7-8a08-3cdd41963c67" });

            migrationBuilder.UpdateData(
                table: "NewsArticles",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsApproved",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "NewsArticles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b42e87ce-97e5-4876-b925-b235969dfc52", "AQAAAAEAACcQAAAAEACwqKuooOsgo9Q4/io/Q6sS8rjPJh6gQIkBUZGlPBZ6RGoHHaPyVXF2VGdLqUMyzw==", "cb1df92c-3ccf-475f-86b9-332c8ec93bce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c63c7141-c3d4-4279-b24b-48013a5b8816", "AQAAAAEAACcQAAAAELg4B9QhKHm+A/07MYvV+ofr9D+lcPGo0eUA+0JueZcijwxSNgdEHDZpyei4TMdElw==", "dd4ee375-3b9d-4336-9aef-fb141a359aa7" });
        }
    }
}
