using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "News article identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "News article title"),
                    Content = table.Column<string>(type: "nvarchar(1300)", maxLength: 1300, nullable: false, comment: "News article content text"),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "News article image URL"),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "News article published on")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticles", x => x.Id);
                },
                comment: "News article entity");

            migrationBuilder.CreateTable(
                name: "NewsArticleReadStatus",
                columns: table => new
                {
                    NewsArticleId = table.Column<int>(type: "int", nullable: false, comment: "News article identifier"),
                    ReaderId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User identifier"),
                    Read = table.Column<bool>(type: "bit", nullable: false, comment: "Read status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticleReadStatus", x => new { x.NewsArticleId, x.ReaderId });
                    table.ForeignKey(
                        name: "FK_NewsArticleReadStatus_AspNetUsers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsArticleReadStatus_NewsArticles_NewsArticleId",
                        column: x => x.NewsArticleId,
                        principalTable: "NewsArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "News article read status entity");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46ef610e-1506-4bdb-bae8-b65b7038bf24", "AQAAAAEAACcQAAAAEKgWo16Neknj2Wp2mJsLp2E0oTJhiaQlEVntFlh3Vn9Nam/4gT4N1qU+gNJTBpe1TA==", "db7a1d0f-8baf-43bb-9b83-02fa60991b6e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81c4877a-50dc-4864-9225-471e90faf779", "AQAAAAEAACcQAAAAEDpHxVonNz2h1aUhP3F+ycIbb18zV8WVymBWs9KQGaGv/bLOxj3hG12YxIqZsXNeLg==", "eb65bd3c-88b0-41a7-a308-154688e7f11f" });

            migrationBuilder.InsertData(
                table: "NewsArticles",
                columns: new[] { "Id", "Content", "ImageUrl", "PublishedOn", "Title" },
                values: new object[,]
                {
                    { 1, "With the start of a new study term, students are gearing up for a month-long examination period. Across educational institutions, there's a mix of anticipation and determination as learners of all ages prepare to showcase their knowledge.\r\n\r\nProfessors are finalizing exam schedules, ensuring students are ready for comprehensive assessments. Students are employing various study techniques, from late-night sessions to group study, to maximize their performance.\r\n\r\nThroughout the month, students will face challenges, but with determination, they're poised to excel. It's a time of growth and resilience as students strive for success in their academic endeavors.", "https://www.inspiringinterns.com/blog/wp-content/uploads/2017/05/time-481447-1200x849.jpg", new DateTime(2024, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Study Term Begins: Students Prepare for Month-Long Exams" },
                    { 2, "In a significant stride towards enhancing educational opportunities, a local programming firm has stepped forward to sponsor a state-of-the-art computer facility. This initiative aims to empower students with access to cutting-edge technology and resources, thereby enriching their learning experience.\r\n\r\nWith the new facility, students can delve deeper into computer science and technology, exploring programming languages, software development, and digital literacy. The firm's sponsorship ensures that students receive a quality education, equipping them with essential skills for the modern workforce.\r\n\r\nThrough this partnership, the community sees the fusion of education and industry, paving the way for innovation and growth. It's a testament to the firm's commitment to nurturing talent and fostering development at the grassroots level.", "https://www.iq.harvard.edu/sites/projects.iq.harvard.edu/files/styles/os_files_xlarge/public/harvard-iqss/files/img_0338.jpeg?m=1585165451&itok=V7MO9V-b", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Local Programming Firm Sponsors Computer Facility, Elevating Education Standards" },
                    { 3, "In a heartwarming gesture to celebrate Dimitar's Name Day, the student board organized a delightful event filled with the aroma of freshly baked banitza. As part of the tradition, the student board members meticulously prepared homemade banitza, to honor Dimitar and bring joy to the campus community. The event, held in the university courtyard, attracted a crowd eager to indulge in this cherished delicacy. With laughter, students savored each bite of the warm banitza, symbolizing the spirit of togetherness and cultural pride. Amidst the festivities, professors expressed gratitude to the student board for their thoughtful gesture, emphasizing the importance of preserving cultural traditions within the university.", "https://www.nakotlona.bg/wp-content/uploads/2021/01/-%D0%B1%D0%B0%D0%BD%D0%B8%D1%86%D0%B0-e1609604417802.jpg", new DateTime(2023, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Student Board Celebrates Dimitar's Name Day with Homemade Banitza" },
                    { 4, "Programming firms are opening their doors to students, offering an unique opportunity to explore career possibilities and gain valuable insights into the tech industry. The \"Day of Open Doors\" initiative aims to bridge the gap between academia and industry, allowing students to interact with professionals, showcase their skills, and learn about job opportunities.\r\n\r\nDuring these events, students are encouraged to bring their CVs and portfolios, ready to engage with recruiters and hiring managers. From software development to project management, companies present a range of roles available, catering to various skill levels and interests within the field of technology.\r\n\r\nIn addition to networking and recruitment opportunities, students can participate in workshops, panel discussions, and tech demos, gaining firsthand knowledge about the latest trends and technologies shaping the industry. Seasoned professionals are on hand to offer advice, mentorship, and career guidance, empowering students to make informed decisions about their future careers.\r\n\r\nThe \"Day of Open Doors\" serves as a platform for collaboration and knowledge exchange, fostering connections between aspiring technologists and industry leaders.", "https://www.asid.org/img/cache/events/Events/6964/lead_image/145229-720-405f-0dee4087947fd39207a5bb29630aac0b", new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Programming Firms Open Doors to Students: Explore Opportunities and Get Career Advice" }
                });

            migrationBuilder.InsertData(
                table: "NewsArticleReadStatus",
                columns: new[] { "NewsArticleId", "ReaderId", "Read" },
                values: new object[] { 1, "0e90dbeb-6468-4abc-9599-b4757e3874aa", true });

            migrationBuilder.InsertData(
                table: "NewsArticleReadStatus",
                columns: new[] { "NewsArticleId", "ReaderId", "Read" },
                values: new object[] { 2, "0e90dbeb-6468-4abc-9599-b4757e3874aa", true });

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticleReadStatus_ReaderId",
                table: "NewsArticleReadStatus",
                column: "ReaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsArticleReadStatus");

            migrationBuilder.DropTable(
                name: "NewsArticles");

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
    }
}
