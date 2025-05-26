using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goodreads.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSocialToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Social_Facebook",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Social_Linkedin",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Social_Twitter",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Social_Facebook",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Social_Linkedin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Social_Twitter",
                table: "AspNetUsers");
        }
    }
}
