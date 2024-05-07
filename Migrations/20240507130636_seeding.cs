using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApp.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "04f2aa4d-8a85-4812-82d3-608cf35a7f3a", "4b78f4b2-1cec-4b55-bfb5-b2c8fe1b026e", "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2cd4d24b-fb57-4fb4-9e5d-706a3bff84aa", "4d2d7696-0d27-465f-a1c4-1836b38f7a15", "user", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04f2aa4d-8a85-4812-82d3-608cf35a7f3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2cd4d24b-fb57-4fb4-9e5d-706a3bff84aa");
        }
    }
}
