using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSkillFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Skill");

            migrationBuilder.RenameColumn(
                name: "ProficiencyLevel",
                table: "Skill",
                newName: "YearsOfExperience");

            migrationBuilder.AddColumn<int>(
                name: "Proficiency",
                table: "Skill",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Proficiency",
                table: "Skill");

            migrationBuilder.RenameColumn(
                name: "YearsOfExperience",
                table: "Skill",
                newName: "ProficiencyLevel");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Skill",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
