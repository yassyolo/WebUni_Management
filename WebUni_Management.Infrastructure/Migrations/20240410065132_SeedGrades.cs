using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedGrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Grade",
                table: "SubjectForStudent",
                type: "float",
                nullable: true,
                comment: "Grade");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c8da985-7e57-42e0-865c-a95d9e4858d2", "AQAAAAEAACcQAAAAEFPXP9akaUxMOLiOuLHpdUY8TEcyTxXE8VjaNfFNsRro3kvYVvBL8rUQul8SGk6OEw==", "49e3d0f3-b632-40d4-b5d4-80dde4c52460" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af2bc163-4b8e-4949-bb13-a2c8f5db61a1", "AQAAAAEAACcQAAAAEGFAV3NAqcqQlAcLcw5z9GHxXib2VCtKi1s5C2RxNel6PASCqF+D+oN6YZzZGP3TSQ==", "17d9c542-e67a-4cb5-b6e0-91b8d0467366" });

            migrationBuilder.UpdateData(
                table: "SubjectForStudent",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { "0e90dbeb-6468-4abc-9599-b4757e3874aa", 1 },
                column: "Grade",
                value: 5.5);

            migrationBuilder.UpdateData(
                table: "SubjectForStudent",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { "0e90dbeb-6468-4abc-9599-b4757e3874aa", 2 },
                column: "Grade",
                value: 5.75);

            migrationBuilder.UpdateData(
                table: "SubjectForStudent",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { "0e90dbeb-6468-4abc-9599-b4757e3874aa", 3 },
                column: "Grade",
                value: 6.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "SubjectForStudent");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04cf301b-b301-490a-b96f-aaf84857a256", "AQAAAAEAACcQAAAAEEyO9uYralcxq5fmoVsmjaEuZKARtxEYee6437GTd11EZb88d5UXMf87CywcPC17qQ==", "0907dde0-b18c-4c0a-b52a-5a1340ba2632" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a600a86-f4d5-47e8-90bf-ebc8fc3ddb4b", "AQAAAAEAACcQAAAAEDMTVDNOmzH0lE79AGugaGrBRVIVGwUnpW7juXwnWTGjEnv/9ho80Rq5RpV4G48VJQ==", "8417db81-0724-4ae7-8491-8a31092b9be0" });
        }
    }
}
