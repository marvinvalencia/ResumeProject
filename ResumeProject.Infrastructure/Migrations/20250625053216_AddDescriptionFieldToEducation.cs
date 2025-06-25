using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionFieldToEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Education",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Education");
        }
    }
}
