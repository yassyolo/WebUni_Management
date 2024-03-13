using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "25b7786d-75f0-42a0-94a5-64eef4ca93a6", "0e90dbeb-6468-4abc-9599-b4757e3874aa" },
                    { "02853dfe-8461-47a5-b545-8aab884099a3", "b242640e-291a-4de7-9701-e3e8e0afb0c9" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f9bbc13-84c3-48be-a432-a3da2dc61c99", "AQAAAAEAACcQAAAAEBNh2DgI+rc0nbaHIkgNW4ATp3ZFQsyB/ERhvK189q0YCg/IVihHV4bBEcmzfh2fKA==", "2868b577-3c3d-481c-903f-7ca6968a8654" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62365043-eda2-494e-a764-5f23250d9b7d", "AQAAAAEAACcQAAAAEO8P5tIyL/LFOP0mnQAOod9dn2BLTIni9i9+mpm8BdJ38uKJuEd/oCc+KEILnxKp2g==", "0b5970b5-e9b9-4162-92fa-7ce9d4fc6656" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "25b7786d-75f0-42a0-94a5-64eef4ca93a6", "0e90dbeb-6468-4abc-9599-b4757e3874aa" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "02853dfe-8461-47a5-b545-8aab884099a3", "b242640e-291a-4de7-9701-e3e8e0afb0c9" });

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8fe29ef0-1836-4bae-bcb9-d711a87a2808", "AQAAAAEAACcQAAAAEOjP4JGlFifGx8T6GTwoq9ILsOfgN0R+NrIGA/wW0klS7oEJH4vFZ5KQt9E6RJ+B5w==", "66ca5005-90d9-4476-a7ef-5a47f5ee9247" });
        }
    }
}
