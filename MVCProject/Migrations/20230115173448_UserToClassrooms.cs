using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCProject.Migrations
{
    public partial class UserToClassrooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserToClassrooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClassroomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToClassrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToClassrooms_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserToClassrooms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserToClassrooms_ClassroomId",
                table: "UserToClassrooms",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToClassrooms_UserId",
                table: "UserToClassrooms",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToClassrooms");
        }
    }
}
