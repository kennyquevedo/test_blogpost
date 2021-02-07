using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogPost.AppCore.Migrations
{
    public partial class statusid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostStatusId",
                schema: "Post",
                table: "Post",
                newName: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusId",
                schema: "Post",
                table: "Post",
                newName: "PostStatusId");
        }
    }
}
