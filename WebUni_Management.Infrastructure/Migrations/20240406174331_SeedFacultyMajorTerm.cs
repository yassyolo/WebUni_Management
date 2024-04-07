using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedFacultyMajorTerm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Students_StudentId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_StudentId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Subject");

            migrationBuilder.AddColumn<int>(
                name: "CourseTermId",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Subject course term identifier");

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Subject faculty identifier");

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Subject major identifier");

            migrationBuilder.AddColumn<int>(
                name: "CourseTermId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Student course term identifier");

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Student faculty identifier");

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Student major identifier");

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Faculty identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Faculty name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.Id);
                },
                comment: "Faculty entity");

            migrationBuilder.CreateTable(
                name: "StudentSubject",
                columns: table => new
                {
                    StudentsId = table.Column<int>(type: "int", nullable: false),
                    SubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubject", x => new { x.StudentsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_StudentSubject_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubject_Subject_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Major",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Major identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Major name"),
                    FacultyId = table.Column<int>(type: "int", nullable: false, comment: "Faculty identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Major", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Major_Faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Major entity");

            migrationBuilder.CreateTable(
                name: "CourseTerm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Course term identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Course term name"),
                    MajorId = table.Column<int>(type: "int", nullable: false, comment: "Course major identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTerm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTerm_Major_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Major",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Course term entity");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f9d4938-da48-4232-afb4-f89585e8eecf", "AQAAAAEAACcQAAAAEETBcT9krRQs6e3kCZ5RgPEmeGJKt8V9nQe1foeT2vz6KhkM8RT2f6d+rciul5RGYQ==", "2b1d4aa0-f914-42b7-a766-ab201e006efc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c818326-a858-47d5-bfa2-135c208ed713", "AQAAAAEAACcQAAAAEBTXuy6oETXAGTYBpFDxTSZoCaG7C5ddOZqYDP7KOU/ecF5JVKON6acyQdwdM2AJoQ==", "845c83e3-7613-442a-8aa7-db3c7e466b55" });

            migrationBuilder.InsertData(
                table: "Faculty",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Mathematics" });

            migrationBuilder.InsertData(
                table: "Major",
                columns: new[] { "Id", "FacultyId", "Name" },
                values: new object[] { 1, 1, "Computer Science" });

            migrationBuilder.InsertData(
                table: "CourseTerm",
                columns: new[] { "Id", "MajorId", "Name" },
                values: new object[] { 1, 1, "Summer 24" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CourseTermId", "FacultyId", "MajorId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.UpdateData(
                table: "Subject",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CourseTermId", "FacultyId", "MajorId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.UpdateData(
                table: "Subject",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CourseTermId", "FacultyId", "MajorId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.UpdateData(
                table: "Subject",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CourseTermId", "FacultyId", "MajorId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_CourseTermId",
                table: "Subject",
                column: "CourseTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_FacultyId",
                table: "Subject",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_MajorId",
                table: "Subject",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseTermId",
                table: "Students",
                column: "CourseTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MajorId",
                table: "Students",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTerm_MajorId",
                table: "CourseTerm",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Major_FacultyId",
                table: "Major",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_SubjectsId",
                table: "StudentSubject",
                column: "SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_CourseTerm_CourseTermId",
                table: "Students",
                column: "CourseTermId",
                principalTable: "CourseTerm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculty_FacultyId",
                table: "Students",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Major_MajorId",
                table: "Students",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_CourseTerm_CourseTermId",
                table: "Subject",
                column: "CourseTermId",
                principalTable: "CourseTerm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Faculty_FacultyId",
                table: "Subject",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Major_MajorId",
                table: "Subject",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_CourseTerm_CourseTermId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculty_FacultyId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Major_MajorId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_CourseTerm_CourseTermId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Faculty_FacultyId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Major_MajorId",
                table: "Subject");

            migrationBuilder.DropTable(
                name: "CourseTerm");

            migrationBuilder.DropTable(
                name: "StudentSubject");

            migrationBuilder.DropTable(
                name: "Major");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropIndex(
                name: "IX_Subject_CourseTermId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_FacultyId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_MajorId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Students_CourseTermId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FacultyId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_MajorId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CourseTermId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "CourseTermId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Subject",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d076ff9b-3318-4072-bf52-9ce70018a4d9", "AQAAAAEAACcQAAAAEI5VLpvz4iklAqNas5fmQjtij70EELZmRySctH/CnyOivSnDHF4e8r1XgI7YD/pNMQ==", "af9f59c4-eedd-49ba-ab10-2df976aaf746" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80c54a8d-305b-4236-9a27-44d8f44db4c1", "AQAAAAEAACcQAAAAEKmYHNSGNEfNL/xwEiNbWJSS7WHDOOtSC+GhSdukKEzeyGaC/fUCpxM0+khAaNukjg==", "b691408b-f204-4f57-9d78-3eaf6fb12530" });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_StudentId",
                table: "Subject",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Students_StudentId",
                table: "Subject",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
