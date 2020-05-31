using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.API.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthData", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"), new DateTimeOffset(new DateTime(1997, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "上海", "author1@xxx.com", "Author 1" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthData", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"), new DateTimeOffset(new DateTime(1998, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "深圳", "author2@xxx.com", "Author 2" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Pages", "Title" },
                values: new object[,]
                {
                    { new Guid("7d8ebda9-2634-4c0f-9469-0695d6132153"), new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"), "Description of Book 1", 281, "Book 1" },
                    { new Guid("7d8ebda9-2634-4c0f-9469-0695d6132154"), new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"), "Description of Book 2", 370, "Book 2" },
                    { new Guid("7d8ebda9-2634-4c0f-9469-0695d6132155"), new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"), "Description of Book 3", 229, "Book 3" },
                    { new Guid("7d8ebda9-2634-4c0f-9469-0695d6132156"), new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"), "Description of Book 4", 440, "Book 4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7d8ebda9-2634-4c0f-9469-0695d6132153"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7d8ebda9-2634-4c0f-9469-0695d6132154"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7d8ebda9-2634-4c0f-9469-0695d6132155"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7d8ebda9-2634-4c0f-9469-0695d6132156"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"));
        }
    }
}
