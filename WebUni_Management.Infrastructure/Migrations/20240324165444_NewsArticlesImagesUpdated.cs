using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUni_Management.Infrastructure.Migrations
{
    public partial class NewsArticlesImagesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bed40286-dc1a-418d-9a18-131583cc210e", "AQAAAAEAACcQAAAAEKvLgw4LJkAUoO36iskzfssE5apwMRPSGo5O47ENDfubeqU+qPutDEjHXrUVeAO0+w==", "370bdb0e-2f1e-4dec-830a-d8d4bc877ebb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1079695e-3b1a-4447-a16e-bc538718fc65", "AQAAAAEAACcQAAAAEL+RZXu74vvtvQj6RM2hQK4jigz5FQJLhxudriOXvl10XUD12BOVovzU+baoSFQSFg==", "cf6e4d78-4942-45d4-94a7-21c8243b1456" });

            migrationBuilder.UpdateData(
                table: "NewsArticles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrl", "PublishedOn" },
                values: new object[] { "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a5/Contemporary_Computer_Lab.jpg/1920px-Contemporary_Computer_Lab.jpg", new DateTime(2024, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "NewsArticles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrl", "PublishedOn" },
                values: new object[] { "https://www.iq.harvard.edu/sites/projects.iq.harvard.edu/files/styles/os_files_xlarge/public/harvard-iqss/files/img_0338.jpeg?m=1585165451&itok=V7MO9V-b", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
