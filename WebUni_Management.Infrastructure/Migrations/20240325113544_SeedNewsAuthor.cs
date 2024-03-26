using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedNewsAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "NewsArticles",
                type: "int",
                nullable: true,
                comment: "News article author identifier");

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

            migrationBuilder.UpdateData(
                table: "NewsArticles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AuthorId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticles_AuthorId",
                table: "NewsArticles",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticles_Students_AuthorId",
                table: "NewsArticles",
                column: "AuthorId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticles_Students_AuthorId",
                table: "NewsArticles");

            migrationBuilder.DropIndex(
                name: "IX_NewsArticles_AuthorId",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "NewsArticles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "19d38de7-9434-4cc2-9a86-6ffddd1de460", "AQAAAAEAACcQAAAAEF6TdiQlstjPgtysXV7z6hvmKjM8aOmlI5DywtVWKCpe1fNgvUb4ZZ3Xs2yR+ubnOw==", "60b59145-5629-441a-ac55-107810e0fde7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "86f5a68f-4ac7-4774-bf46-d42f0e09a4e8", "AQAAAAEAACcQAAAAEJXlz8Ca2mIKwYfvsuja+HMPVE1LwHx+Pj/lG9WayID7bBNPJDVKLIguIyizCaR6vA==", "5063a801-440e-42e0-8f50-c853f2a981b9" });
        }
    }
}
