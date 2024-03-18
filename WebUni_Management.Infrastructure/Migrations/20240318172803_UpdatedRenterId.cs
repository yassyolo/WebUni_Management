using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class UpdatedRenterId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RenterId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true,
                comment: "Book renter identifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd37f9d2-6b2d-4afa-bad4-a50762f95d96", "AQAAAAEAACcQAAAAEGaCjeN79kNsGvfsHM1cm5pqIlnSGoal1YATyTcUSeRLuE17RiKVk/58npNCUKtBXQ==", "b2ba92ed-87ac-4017-a1f0-a933f264e6e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d96837fa-cbb8-4380-bff3-999e842e8f93", "AQAAAAEAACcQAAAAEGl//QED4CKYtvkdUU2JDLCgr8ckT+Claa0QfS1CyZs3tsZ6Cm89aN+amKZWw+Zuwg==", "2b93f488-67ff-4776-97fe-56705d5abd22" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "RenterId",
                value: "0e90dbeb-6468-4abc-9599-b4757e3874aa");

            migrationBuilder.CreateIndex(
                name: "IX_Books_RenterId",
                table: "Books",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_RenterId",
                table: "Books",
                column: "RenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_RenterId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_RenterId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "RenterId",
                table: "Books");

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
        }
    }
}
