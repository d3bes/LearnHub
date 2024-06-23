using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHub.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "enrollment",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    studentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    courseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enrollment", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_enrollment_AspNetUsers_studentId",
                        column: x => x.studentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_enrollment_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_enrollment_courseId",
                table: "enrollment",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_enrollment_studentId",
                table: "enrollment",
                column: "studentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enrollment");
        }
    }
}
