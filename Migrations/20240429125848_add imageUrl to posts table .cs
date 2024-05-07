using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApp.Migrations
{
    public partial class addimageUrltopoststable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostImageUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostImageUrl",
                table: "Posts");
        }
    }
}
