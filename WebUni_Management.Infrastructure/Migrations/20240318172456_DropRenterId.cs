using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class DropRenterId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Students_RenterId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_RenterId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "RenterId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30306c81-ebc9-4078-9a76-bfbbc3e9b6d5", "AQAAAAEAACcQAAAAEM+KgL9jtz3BAw6az3sxiIfEumitWXTlMJWjEvk6nlw4+TDtgYXk/zpiU4NX9RrYjg==", "2892f0a3-1b32-4df2-a2d5-0f8eed86d99c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff2bba3e-5539-4143-ac72-ce6ea169a480", "AQAAAAEAACcQAAAAEATjZkoL+MzNOTVfR5fkk04IiYBF4fmaKtChGJUlxDAvaBwXrPyVxg5zFU1/485Pag==", "da7f95b2-a836-4b7b-9ce0-b0f00f180dc4" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_StudentId",
                table: "Books",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Students_StudentId",
                table: "Books",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Students_StudentId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_StudentId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "RenterId",
                table: "Books",
                type: "int",
                nullable: true,
                comment: "Book renter id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b6bbaf8-c26f-4fca-9e97-c41901ed133a", "AQAAAAEAACcQAAAAEBZs2XQUAu6jua9gp+VXZ1HrWvk5KtECJqXOzE9FzLi5Q8lPNNkffpwxsG8V7e2gxQ==", "69392bf2-8d7a-43e5-b9a1-540694e5ffb3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd1328a8-3b4d-40b6-8d8e-45ed682f14e9", "AQAAAAEAACcQAAAAEGUlW2f3YDq8gzDN+egSj7Mz73KRfuDcnSrlLAG36LhZjDOLJNTJYoaM97/Y1wQMlQ==", "4f610bc7-7b88-42d2-aefa-910d132ea220" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "RenterId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Books_RenterId",
                table: "Books",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Students_RenterId",
                table: "Books",
                column: "RenterId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
