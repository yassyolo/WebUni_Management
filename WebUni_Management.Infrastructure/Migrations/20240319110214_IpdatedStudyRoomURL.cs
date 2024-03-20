using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class IpdatedStudyRoomURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentalTime",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Rental time");

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

            migrationBuilder.UpdateData(
                table: "StudyRooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://st.hzcdn.com/simgs/pictures/home-offices/calender-allen-architecture-llc-img~cd81328d0bb27f02_8-0752-1-86bb54d.jpg");

            migrationBuilder.UpdateData(
                table: "StudyRooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://st.hzcdn.com/simgs/pictures/home-offices/white-and-airy-jennifer-pacca-interiors-img~387173790a9ee6b8_8-4497-1-a0376b0.jpg");

            migrationBuilder.UpdateData(
                table: "StudyRooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://st.hzcdn.com/simgs/pictures/home-offices/eclectic-and-colorful-greensboro-nc-jessica-dauray-interiors-elements-of-style-img~362195d10a0dcf59_8-0725-1-24dcf75.jpg");

            migrationBuilder.UpdateData(
                table: "StudyRooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://st.hzcdn.com/simgs/pictures/home-offices/contemporary-home-office-tazz-lighting-inc-img~259113440b895f26_8-5371-1-da12f0e.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalTime",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "148033d0-9851-460f-803a-752806f99cf2", "AQAAAAEAACcQAAAAEM5k/VZpVFIHTRPHMJ05knXWQzDNBdFCnCVGO8TjaJZO8s5a9f07Ni5uJL92maQgTA==", "59961437-1670-427d-b49c-041dadd08354" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b9c20f7-0624-4028-9eb0-eb59d1f35d28", "AQAAAAEAACcQAAAAELUHPvgP8YpP2jL5Q3RPdnzlqC+bOwPnCPl6k2qhntOtBghYUa+yAZ/HXYrFdlCZUg==", "ba9b8efd-896b-49bb-9086-98dde78a38db" });

            migrationBuilder.UpdateData(
                table: "StudyRooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.houzz.com/photos/calender-transitional-home-office-dallas-phvw-vp~129507155");

            migrationBuilder.UpdateData(
                table: "StudyRooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://www.houzz.com/photos/white-and-airy-contemporary-home-office-new-york-phvw-vp~114130821");

            migrationBuilder.UpdateData(
                table: "StudyRooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://www.houzz.com/photos/eclectic-and-colorful-greensboro-nc-transitional-home-office-phvw-vp~106627929");

            migrationBuilder.UpdateData(
                table: "StudyRooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://www.houzz.com/photos/contemporary-home-office-contemporary-home-office-san-diego-phvw-vp~127035222");
        }
    }
}
