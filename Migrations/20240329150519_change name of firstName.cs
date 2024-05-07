using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApp.Migrations
{
    public partial class changenameoffirstName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FristName",
                table: "AspNetUsers",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "FristName");
        }
    }
}
