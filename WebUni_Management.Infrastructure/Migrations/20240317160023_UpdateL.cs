using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class UpdateL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Library_LibraryId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "LibraryId",
                table: "Books",
                type: "int",
                nullable: true,
                defaultValue: 1,
                comment: "Library identifier",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.InsertData(
                table: "BookByBookAuthors",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 6, 5 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "LibraryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "LibraryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "LibraryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "LibraryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "LibraryId",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Library_LibraryId",
                table: "Books",
                column: "LibraryId",
                principalTable: "Library",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Library_LibraryId",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "BookByBookAuthors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.AlterColumn<int>(
                name: "LibraryId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1,
                oldComment: "Library identifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "992cc882-3e16-4ff8-b3bc-98a3ca68beeb", "AQAAAAEAACcQAAAAECGR+s8zT3t6ztFOPKHvxw/4A5kXKto27u37FRuUZ3eS7Qt5w+8m7tKp4OpDnvWg8Q==", "d26df063-022a-43ec-b2df-2434931de4ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91e59b14-3e32-4044-b22b-a66fe0d001c6", "AQAAAAEAACcQAAAAEEaiTNKNr9S4Hyd2k65lvB9FrPcqDYB5UX5OMlTuIwK7ghww5MSLASur2nwRpu+nFQ==", "9eb0c512-3d3d-4da8-8506-3e12643ebdd3" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "LibraryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "LibraryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "LibraryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "LibraryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "LibraryId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Library_LibraryId",
                table: "Books",
                column: "LibraryId",
                principalTable: "Library",
                principalColumn: "Id");
        }
    }
}
