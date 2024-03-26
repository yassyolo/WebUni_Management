using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bed40286-dc1a-418d-9a18-131583cc210e", "AQAAAAEAACcQAAAAEKvLgw4LJkAUoO36iskzfssE5apwMRPSGo5O47ENDfubeqU+qPutDEjHXrUVeAO0+w==", "370bdb0e-2f1e-4dec-830a-d8d4bc877ebb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1079695e-3b1a-4447-a16e-bc538718fc65", "AQAAAAEAACcQAAAAEL+RZXu74vvtvQj6RM2hQK4jigz5FQJLhxudriOXvl10XUD12BOVovzU+baoSFQSFg==", "cf6e4d78-4942-45d4-94a7-21c8243b1456" });
        }
    }
}
