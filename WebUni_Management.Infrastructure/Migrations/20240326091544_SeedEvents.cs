using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Event identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestParticipant = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "Guest participant for the event"),
                    Name = table.Column<string>(type: "nvarchar(110)", maxLength: 110, nullable: false, comment: "Event name"),
                    Description = table.Column<string>(type: "nvarchar(1100)", maxLength: 1100, nullable: false, comment: "Event description"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Event start time"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Event end time"),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Event image URL"),
                    Capacity = table.Column<int>(type: "int", nullable: false, comment: "Event capacity")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                },
                comment: "Event entity");

            migrationBuilder.CreateTable(
                name: "EventParticipant",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false, comment: "Event identifier"),
                    ParticipantId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Participant identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipant", x => new { x.EventId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_EventParticipant_AspNetUsers_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventParticipant_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Event participant entity");

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

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "Description", "EndTime", "GuestParticipant", "ImageUrl", "Name", "StartTime" },
                values: new object[,]
                {
                    { 1, 50, "Embark on a journey into the realm of software development with our captivating seminar on \"Why Choose C#.\" Led by a seasoned industry expert, this seminar delves deep into the myriad benefits and advantages of utilizing C# as your programming language of choice. From its robust object-oriented design to its versatility in application development, C# offers unparalleled opportunities for both novice and seasoned developers alike. Join us as we uncover the power and potential of C#, and discover why it remains a top choice in the ever-evolving landscape of technology. Reserve your seat now for an enlightening experience you won't want to miss!", new DateTime(2024, 4, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), "John Doe", "https://ardounco.sirv.com/WP_content.bytehide.com/2022/03/why-learn-csharp.png", "Exploring the Power of C#", new DateTime(2024, 4, 15, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 65, "Join me for an insightful seminar where I delve into the fascinating world of embedded technologies and the Internet of Things (IoT). Discover how embedded systems are revolutionizing various industries, from smart homes to industrial automation. Learn about the latest trends, challenges, and opportunities in the realm of IoT, and explore real-world applications that are shaping the future of technology. Whether you're a seasoned engineer or an enthusiast curious about the possibilities of connected devices, this seminar promises to expand your knowledge and inspire innovation.", new DateTime(2024, 5, 2, 18, 0, 0, 0, DateTimeKind.Unspecified), "Jane Dimova", "https://builtin.com/sites/www.builtin.com/files/styles/og/public/2022-08/connected-devices-internet-of-things-iot-devices.png", "Exploring Embedded Technologies and IoT Seminar", new DateTime(2024, 5, 2, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 30, "Join us for an illuminating seminar as we explore the profession of QA (Quality Assurance) Tester and its pivotal role in ensuring the quality of software products. Gain insights into the responsibilities, methodologies, and best practices employed by QA testers to identify bugs, verify functionality, and enhance user experience. Discover how QA testing contributes to the success of software projects by mitigating risks and improving product reliability. Whether you're an aspiring QA tester or simply curious about the behind-the-scenes of software development, this seminar offers a comprehensive overview of the QA profession and its significance in delivering high-quality software.", new DateTime(2024, 3, 30, 15, 0, 0, 0, DateTimeKind.Unspecified), "Boris Cholakov", "https://testpro.io/wp-content/uploads/2023/11/qa-tester.jpg", "Unveiling the Role of QA Tester: Ensuring Quality in Software Development", new DateTime(2024, 3, 30, 11, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "EventParticipant",
                columns: new[] { "EventId", "ParticipantId" },
                values: new object[] { 1, "0e90dbeb-6468-4abc-9599-b4757e3874aa" });

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipant_ParticipantId",
                table: "EventParticipant",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventParticipant");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d51ddd1d-ea53-4637-89cf-680841d35354", "AQAAAAEAACcQAAAAEOCOZxobV+1YNm7soqtCQ6bKGKrlqPQTB/p2ZdATelPQS71BGaPPIjsioHgQPgnQBA==", "7cd26622-8795-432f-8ef8-a73f21d16848" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bb0da00-51a7-403a-a381-e3bf0348bba8", "AQAAAAEAACcQAAAAEI6Zjx9/X7hmB/uHoUXGZKUNdhuI25FQU9cBPWngJ/TV7w1aLstcdZl85d/+VpBXoQ==", "1b9f75bb-f25c-4db7-8a08-3cdd41963c67" });
        }
    }
}
