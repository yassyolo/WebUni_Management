using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class SeedLibrary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Book author identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Book author first name"),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Book author last name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => x.Id);
                },
                comment: "Book author entity");

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Book category identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Book category name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => x.Id);
                },
                comment: "Book category entity");

            migrationBuilder.CreateTable(
                name: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Library identifier")
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library", x => x.Id);
                },
                comment: "Library entity");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Book Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Book title"),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Book image URL"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Book category identifier"),
                    PublishYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false, comment: "Book publish year"),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, comment: "Book description"),
                    IsRented = table.Column<bool>(type: "bit", nullable: false, comment: "Is book rented"),
                    RenterId = table.Column<int>(type: "int", nullable: true, comment: "Book renter id"),
                    RentalDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Rent date of the book"),
                    LibraryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BookCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Library_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Library",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_Students_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Book entity");

            migrationBuilder.CreateTable(
                name: "BookBookAuthor",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    BooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBookAuthor", x => new { x.AuthorId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_BookBookAuthor_BookAuthors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "BookAuthors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBookAuthor_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookByBookAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false, comment: "Book identifier"),
                    AuthorId = table.Column<int>(type: "int", nullable: false, comment: "Book author identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookByBookAuthors", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BookByBookAuthors_BookAuthors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "BookAuthors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookByBookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Book by book author entity");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "992cc882-3e16-4ff8-b3bc-98a3ca68beeb", "AQAAAAEAACcQAAAAECGR+s8zT3t6ztFOPKHvxw/4A5kXKto27u37FRuUZ3eS7Qt5w+8m7tKp4OpDnvWg8Q==", "d26df063-022a-43ec-b2df-2434931de4ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91e59b14-3e32-4044-b22b-a66fe0d001c6", "AQAAAAEAACcQAAAAEEaiTNKNr9S4Hyd2k65lvB9FrPcqDYB5UX5OMlTuIwK7ghww5MSLASur2nwRpu+nFQ==", "9eb0c512-3d3d-4da8-8506-3e12643ebdd3" });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Natasha", "Maurits" },
                    { 2, "Branislava", "Ćurčić-Blake" },
                    { 3, "Hugh", "Neill" },
                    { 4, "Kingsley", "Augustine" },
                    { 5, "Ramesh", "Chandra" },
                    { 6, "Mani", "Naidu" }
                });

            migrationBuilder.InsertData(
                table: "BookCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mathematics" },
                    { 2, "Physics" },
                    { 3, "Chemistry" }
                });

            migrationBuilder.InsertData(
                table: "Library",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsRented", "LibraryId", "PublishYear", "RentalDate", "RenterId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "'Math for Scientists: Refreshing the Essentials' offers a concise yet comprehensive review of fundamental mathematical concepts essential for scientists.Co - authored by Branislava Ćurčić - Blake and Natalia Maria, this book serves as a valuable resource for refreshing and reinforcing mathematical skills necessary for scientific inquiry.", "https://m.media-amazon.com/images/I/617vHgW8ZhL._SY522_.jpg", true, null, "2017", new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Math for Scientists: Refreshing the Essentials" },
                    { 2, 1, "Master Math effortlessly with this comprehensive guide. Ideal for beginners and intermediates, it features step-by-step explanations, practice questions, and chapter summaries for confident learning. No separate workbooks needed!", "https://m.media-amazon.com/images/I/71EUTt1F2vL._SY522_.jpg", false, null, "2018", null, null, "Mathematics: A Complete Introduction" },
                    { 3, 1, "'Simplified Statistics and Probability' is a comprehensive book designed for high school and college students. It offers clear explanations, numerous examples, and practice exercises with answers for self-assessment, enhancing understanding and proficiency in the subject.", "https://m.media-amazon.com/images/I/61CANeMV8wL._SY522_.jpg", false, null, "2018", null, null, "Simplified Statistics and Probability: A Mathematics Book for High Schools and Colleges" },
                    { 4, 3, "'Basic Organic Chemistry' covers fundamental concepts, organic molecules, functional groups, nomenclature, acids/bases, stereochemistry, amino acids, proteins, carbohydrates, alcohols, ethers, and spectroscopy, offering insights for understanding organic reactions.", "https://m.media-amazon.com/images/I/813VoAjptdL._SY522_.jpg", false, null, "2019", null, null, "Basic Organic Chemistry" },
                    { 5, 2, "'Engineering Physics' caters to first-year undergraduates at Jawaharlal Nehru Technical University. Covering crystallography, quantum mechanics, metals, dielectrics, semiconductors, superconductivity, lasers, holography, nanotechnology, and optics, it employs clear pedagogy for comprehensive learning.", "https://m.media-amazon.com/images/I/81p+3Q5hsvL._SY522_.jpg", false, null, "2010", null, null, "Engineering Physics" }
                });

            migrationBuilder.InsertData(
                table: "BookByBookAuthors",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 3 },
                    { 5, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookBookAuthor_BooksId",
                table: "BookBookAuthor",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_BookByBookAuthors_AuthorId",
                table: "BookByBookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LibraryId",
                table: "Books",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_RenterId",
                table: "Books",
                column: "RenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBookAuthor");

            migrationBuilder.DropTable(
                name: "BookByBookAuthors");

            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "Library");

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
    }
}
