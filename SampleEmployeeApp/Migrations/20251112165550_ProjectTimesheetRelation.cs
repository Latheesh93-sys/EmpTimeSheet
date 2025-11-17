using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleEmployeeApp.Migrations
{
    /// <inheritdoc />
    public partial class ProjectTimesheetRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectEmployeeId",
                table: "Timesheets",
                newName: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Timesheets",
                newName: "ProjectEmployeeId");
        }
    }
}
