using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApp.Migrations
{
    public partial class delettilepropfrompostandupdateimagedatatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostImageUrl",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.AddColumn<byte[]>(
                name: "PostImage",
                table: "Posts",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostImage",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "PostImageUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
