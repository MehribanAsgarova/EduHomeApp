using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeApp.Migrations
{
    public partial class ChangedCourseFuatureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "CourseFeatures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "CourseFeatures");
        }
    }
}
