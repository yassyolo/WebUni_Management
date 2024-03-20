using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class RoomRentalTimeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentalTime",
                table: "StudyRooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Rental time");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73822ce1-82a1-4ea5-9012-406c264f5d94", "AQAAAAEAACcQAAAAEEFVLfQbSIz+1nwxf3cI1wHPhiO+8U5AFWK3pT2wj9Du+/Nb6kLzQoxmUlWGOBJWnQ==", "11bf4b51-bc6d-422b-8cc8-a1b81e859cd9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba078086-402a-405c-87ca-04e327fc6ab3", "AQAAAAEAACcQAAAAEHzEpKnBHeBize4CnIS6Ql50eAPBDdmDNRxnEV8LqBuBn0xJhRgkgW+Z328TCs1+4w==", "213c6314-c9b4-405b-9d93-52698127cc6f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalTime",
                table: "StudyRooms");

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
        }
    }
}
