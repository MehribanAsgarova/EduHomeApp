using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeApp.Migrations
{
    public partial class AddedContactCommentTableChangedContctTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ContactId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactComments_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactComments_ContactId",
                table: "ContactComments",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactComments");
        }
    }
}
