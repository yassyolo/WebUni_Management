using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Canteen identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Canteen date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                },
                comment: "Menu entity");

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Dish identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Dish name"),
                    Category = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "Dish category"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Dish price"),
                    MenuId = table.Column<int>(type: "int", nullable: false, comment: "Menu identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dishes_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Canteen dish entity");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e615d62c-890a-4b34-bbe6-89ebc5b4d519", "AQAAAAEAACcQAAAAEKNttpMVxUJydzAp+NQvuX3DMKBkS7lVhExRwodl/CvlVen8exbTNZK5EY+LQMJxNQ==", "dc64f32e-2a19-4677-8bda-0612f8c84ebe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "284da03d-a2b1-4e9f-8afa-279021950c87", "AQAAAAEAACcQAAAAECKw1ctnloXOm6Qe0yPBCDoKG7MwLj5QYiTVubr5z4oFeFWomeyZ+tif4iIUn+F/pA==", "06ab7db0-ada3-43b3-a4b0-c1dfe8425b51" });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Date" },
                values: new object[] { 1, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Category", "MenuId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Salad", 1, "Greek Salad", 1.00m },
                    { 2, "Salad", 1, "Caesar Salad", 1.50m },
                    { 3, "Main Dish", 1, "Spaghetti Carbonara", 2.00m },
                    { 4, "Main Dish", 1, "Chicken Alfredo", 2.50m },
                    { 5, "Dessert", 1, "Tiramisu", 1.00m },
                    { 6, "Dessert", 1, "Cheesecake", 1.50m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_MenuId",
                table: "Dishes",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d65e173-756b-4d92-b231-75a5b655258f", "AQAAAAEAACcQAAAAEOvuBvszHw/SzmH/y6Sk9nZMHPfPcifTlCa5jdczGlOZQXEFsFP3MBUJC7bYTpCNZA==", "0ca23a04-9213-47ce-8563-792c4a9b999b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a2e5b08-77a8-4399-99ea-9288ef40d750", "AQAAAAEAACcQAAAAEIgMcf17SB5wrxZAV+g5WsT8s+o1PAwLZDS5KEV0JtmhA7QUzDG8zCnh0JPgZb/7Iw==", "eae81e42-a81a-495e-ad92-e0cf50f09aa4" });
        }
    }
}
