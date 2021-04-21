using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeApp.Migrations
{
    public partial class AddedCourseAndCourseFeatureTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<string>(nullable: true),
                    ClassDuration = table.Column<string>(nullable: true),
                    SkillLevel = table.Column<string>(nullable: true),
                    StudentCount = table.Column<int>(nullable: false),
                    Assesment = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(nullable: true),
                    CourseName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    About = table.Column<string>(nullable: true),
                    Apply = table.Column<string>(nullable: true),
                    Certification = table.Column<string>(nullable: true),
                    CourseFeatureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseFeatures_CourseFeatureId",
                        column: x => x.CourseFeatureId,
                        principalTable: "CourseFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseFeatureId",
                table: "Courses",
                column: "CourseFeatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "CourseFeatures");
        }
    }
}
