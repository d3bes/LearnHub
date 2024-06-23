using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHub.EF.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.DropTable(
           name: "enrollments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
