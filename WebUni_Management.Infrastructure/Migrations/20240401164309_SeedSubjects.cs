using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedSubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Subject identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Subject name"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Subject description"),
                    TotlaAttendanceCount = table.Column<int>(type: "int", nullable: false, comment: "Subject attendance times"),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                },
                comment: "Subject entity");

            migrationBuilder.CreateTable(
                name: "SubjectProfessors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Subject Professor identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Professor first name"),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Professor last name"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Professor email"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false, comment: "Professor email"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Professor title"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Professor description")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectProfessors", x => x.Id);
                },
                comment: "Professor of subject entity");

            migrationBuilder.CreateTable(
                name: "SubjectForStudent",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false, comment: "Subject identifier"),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Student identifier"),
                    AttendanceRecord = table.Column<int>(type: "int", nullable: true, comment: "AttendanceRecord")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectForStudent", x => new { x.StudentId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_SubjectForStudent_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectForStudent_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Subject for student entity");

            migrationBuilder.CreateTable(
                name: "SubjectByProfessor",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false, comment: "Subject identifier"),
                    ProfessorId = table.Column<int>(type: "int", nullable: false, comment: "Professor identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectByProfessor", x => new { x.SubjectId, x.ProfessorId });
                    table.ForeignKey(
                        name: "FK_SubjectByProfessor_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectByProfessor_SubjectProfessors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "SubjectProfessors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Subject by professor entity");

            migrationBuilder.CreateTable(
                name: "SubjectSubjectProfessor",
                columns: table => new
                {
                    SubjectProfessorsId = table.Column<int>(type: "int", nullable: false),
                    SubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectSubjectProfessor", x => new { x.SubjectProfessorsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_SubjectSubjectProfessor_Subject_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectSubjectProfessor_SubjectProfessors_SubjectProfessorsId",
                        column: x => x.SubjectProfessorsId,
                        principalTable: "SubjectProfessors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "Id", "Description", "Name", "StudentId", "TotlaAttendanceCount" },
                values: new object[,]
                {
                    { 1, "Calculus is a fundamental branch of mathematics that deals with rates of change and accumulation. It is used in various fields such as physics, engineering, economics, and computer science. This course covers topics such as limits, derivatives, integrals, and applications of calculus in real-world problems.", "Calculus", null, 13 },
                    { 2, "Linear algebra is a branch of mathematics that studies vectors, vector spaces, linear transformations, and systems of linear equations. It is essential in various fields such as physics, engineering, computer science, and economics. This course covers topics such as matrices, determinants, vector spaces, eigenvalues, and eigenvectors.", "Linear Algebra", null, 15 },
                    { 3, "Software architecture is the process of designing and defining the structure of a software system. It involves making high-level decisions about the organization and implementation of software components. This course covers topics such as architectural styles, design patterns, software quality attributes, and system decomposition.", "Software Architecture", null, 12 }
                });

            migrationBuilder.InsertData(
                table: "SubjectProfessors",
                columns: new[] { "Id", "Description", "Email", "FirstName", "LastName", "PhoneNumber", "Title" },
                values: new object[,]
                {
                    { 1, "Maria Ivanova is a dedicated professor with over 10 years of experience in teaching mathematics. She holds a Ph.D. in Mathematics from Sofia University and has published several research papers in the field.", "mivanova@gmail.com", "Maria", "Ivanova", "0888888888", "Professor" },
                    { 2, "Georgi Petrov is an enthusiastic assistant professor specializing in mathematics. He has a Master's degree in Mathematics from the University of Sofia and is passionate about teaching and research.", "gp@gmail.com", "Georgi", "Petrov", "0877777777", "Assistant" },
                    { 3, "Ivan Georgiev is a dedicated professor with a background in mathematics and computer science. He holds a Bachelor's degree in Mathematics and a Master's degree in Computer Science.", "ivg@abv.bg", "Ivan", "Georgiev", "0899999999", "Professor" },
                    { 4, "Stefan Stefanov is a professor specializing in computer science. He has a Master's degree in Computer Science and is passionate about teaching and research.", "ssss@gmail.com", "Stefan", "Stefanov", "0888888883", "Professor" }
                });

            migrationBuilder.InsertData(
                table: "SubjectByProfessor",
                columns: new[] { "ProfessorId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "SubjectForStudent",
                columns: new[] { "StudentId", "SubjectId", "AttendanceRecord" },
                values: new object[,]
                {
                    { "0e90dbeb-6468-4abc-9599-b4757e3874aa", 1, 5 },
                    { "0e90dbeb-6468-4abc-9599-b4757e3874aa", 2, 7 },
                    { "0e90dbeb-6468-4abc-9599-b4757e3874aa", 3, 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_StudentId",
                table: "Subject",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectByProfessor_ProfessorId",
                table: "SubjectByProfessor",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectForStudent_SubjectId",
                table: "SubjectForStudent",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectSubjectProfessor_SubjectsId",
                table: "SubjectSubjectProfessor",
                column: "SubjectsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectByProfessor");

            migrationBuilder.DropTable(
                name: "SubjectForStudent");

            migrationBuilder.DropTable(
                name: "SubjectSubjectProfessor");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "SubjectProfessors");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9db9eab0-65e8-42f1-b7f9-c10ad28e3c30", "AQAAAAEAACcQAAAAEATGBwisZBVsw44SLT9vLk9Dd3S8YyGVdUIYIrSYmqJaoMSotjXDMrjsT2sWYvHA0A==", "69dfbd1a-a4a2-42db-9a43-b4c2d6d2e091" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ee0645c-2a22-49b7-8868-c467f72e493d", "AQAAAAEAACcQAAAAEOOYLzWgBNmp7CQ/afEmlSMRkEZ4Kp9OSRGcV1eRrJJWu10bq1TT8n37u6CIGX7Q4w==", "e5733f17-ec15-4360-b3a9-77f08dc06ea5" });
        }
    }
}
