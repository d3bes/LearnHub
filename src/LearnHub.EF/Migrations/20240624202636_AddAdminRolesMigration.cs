using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHub.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminRolesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.Sql("insert into [dbo].[AspNetUserRoles] ([UserId],[RoleId]) select '07a700e7-6f45-44bf-a4f1-76f5f754fe3f' , id from [dbo].[AspNetRoles]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("delete from [dbo].[AspNetUserRoles] where [UserId] ='07a700e7-6f45-44bf-a4f1-76f5f754fe3f");
        }
    }
}
