using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedStudyRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Study room identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(110)", maxLength: 110, nullable: false, comment: "Study room name"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Study room description"),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Study room image URL"),
                    Floor = table.Column<int>(type: "int", nullable: false, comment: "Study room floor location"),
                    Capacity = table.Column<int>(type: "int", nullable: false, comment: "Study room capacity"),
                    IsRented = table.Column<bool>(type: "bit", nullable: false, comment: "Is study room rented"),
                    RenterId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Study room renter identifier"),
                    RentalDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Rent date of the room"),
                    LibraryId = table.Column<int>(type: "int", nullable: false, comment: "Library identifier"),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyRooms_AspNetUsers_RenterId",
                        column: x => x.RenterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudyRooms_Library_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Library",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyRooms_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                },
                comment: "Study room entity");

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

            migrationBuilder.InsertData(
                table: "StudyRooms",
                columns: new[] { "Id", "Capacity", "Description", "Floor", "ImageUrl", "IsRented", "LibraryId", "Name", "RentalDate", "RenterId", "StudentId" },
                values: new object[,]
                {
                    { 1, 3, "Comfortable, productive space for focused work & collaboration. Equipped with modern amenities to support efficient work sessions. To enhance concentration, the room is designed with sound-absorbing materials to minimize distractions from outside noise.", 1, "https://www.houzz.com/photos/calender-transitional-home-office-dallas-phvw-vp~129507155", false, 1, "Cozy Study Room for Three, a Heaven for Productivity", null, null, null },
                    { 2, 5, "Discover a Serene Study Haven: Our spacious room comfortably accommodates up to 5 people, offering ergonomic seating, ample desk space, and abundant natural light to foster productivity and concentration. Delight in the quiet ambiance and conducive environment for collaborative projects, group discussions, or solitary study sessions. Elevate your learning experience in this peaceful retreat designed for academic excellence and intellectual pursuits.", 2, "https://www.houzz.com/photos/white-and-airy-contemporary-home-office-new-york-phvw-vp~114130821", false, 1, "Study Nook, space for 5, fostering productivity and creativity", null, null, null },
                    { 3, 10, "Step into our expansive study sanctuary designed to accommodate up to 10 individuals. With abundant space, ergonomic furnishings, and a tranquil atmosphere, this room fosters focused study sessions, collaborative brainstorming, and group projects. Elevate your academic pursuits in this premium environment tailored for productivity and intellectual growth.", 3, "https://www.houzz.com/photos/eclectic-and-colorful-greensboro-nc-transitional-home-office-phvw-vp~106627929", false, 1, "Elite Learning Oasis, The Grand Study Room for 10", null, null, null },
                    { 4, 1, "Escape to your own secluded sanctuary for uninterrupted focus and productivity. Our single study room, designed for one individual, offers a tranquil environment with ergonomic furnishings and ample natural light. Dive into your studies, research, or creative projects in complete privacy, free from distractions. Maximize your productivity and achieve your academic or professional goals in this serene haven tailored just for you.", 1, "https://www.houzz.com/photos/contemporary-home-office-contemporary-home-office-san-diego-phvw-vp~127035222", true, 1, "Solitude Haven, Private Study Retreat", new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "0e90dbeb-6468-4abc-9599-b4757e3874aa", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyRooms_LibraryId",
                table: "StudyRooms",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyRooms_RenterId",
                table: "StudyRooms",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyRooms_StudentId",
                table: "StudyRooms",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyRooms");

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
        }
    }
}
