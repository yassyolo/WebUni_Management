using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedProfessors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3c3284e-dfb0-4543-8046-bb77b2daeb29", "AQAAAAEAACcQAAAAEEXlgrwXIAV4M/rtArIo5BBOpVuwduK4p6TE6OlanagA0z2Jbxhyk0RY6KWb5e/Qng==", "73522ee7-77a6-4e05-9e3b-5f5f19d77763" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74204972-be2a-4dfb-a131-9a947545e1d7", "AQAAAAEAACcQAAAAEDDJ0Uw4J1WL0Y9Q7a9UOAF/xpTJPfthsSUej1Wf/wpeRslrsxhLW/B33KGWIAFi9w==", "3e2ddd43-a565-4a75-aba8-3f88e3068592" });

            migrationBuilder.InsertData(
                table: "SubjectProfessors",
                columns: new[] { "Id", "Description", "Email", "FirstName", "LastName", "PhoneNumber", "Title" },
                values: new object[,]
                {
                    { 5, "Ivanka Ivanova , an accomplished Linear Algebra Assistant, graduated with honors from the Mathematics Department at Sofia University \"St. Kliment Ohridski\" in Bulgaria. Her academic journey was marked by a deep fascination with abstract algebraic structures and their applications in various fields. During her undergraduate studies, Ivanka actively participated in research projects focusing on linear algebra and its role in computer science and data analysis.", "i@gmail.com", "Ivanka", "Ivanova", "0888888884", "Assistant" },
                    { 6, "Georgi Georgiev brings a wealth of expertise and practical experience to his teaching role. He obtained his Bachelor's degree in Computer Science from the Technical University of Sofia, Bulgaria, where he developed a strong foundation in software engineering principles and methodologies.", "g@gmail.com", "Georgi", "Georgiev", "0888888885", "Assistant" }
                });

            migrationBuilder.InsertData(
                table: "SubjectByProfessor",
                columns: new[] { "ProfessorId", "SubjectId" },
                values: new object[] { 5, 2 });

            migrationBuilder.InsertData(
                table: "SubjectByProfessor",
                columns: new[] { "ProfessorId", "SubjectId" },
                values: new object[] { 6, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubjectByProfessor",
                keyColumns: new[] { "ProfessorId", "SubjectId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "SubjectByProfessor",
                keyColumns: new[] { "ProfessorId", "SubjectId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "SubjectProfessors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SubjectProfessors",
                keyColumn: "Id",
                keyValue: 6);

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
        }
    }
}
