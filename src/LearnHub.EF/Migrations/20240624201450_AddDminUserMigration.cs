using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHub.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddDminUserMigration : Migration
    {

        
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ( [Id], [Discriminator], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount] ) VALUES('07a700e7-6f45-44bf-a4f1-76f5f754fe3f', 'Admin', 'administartor.UserName', 'ADMINSTRATO.USERNAME', 'admin.email@example.com',  'ADMIN.EMAIL@EXAMPLE.COM', 0, NULL,  0,  0, NULL,  0, 0 );");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.Sql("delete from [dbo].[AspNetUsers]  where id = '07a700e7-6f45-44bf-a4f1-76f5f754fe3f'");
        }
    }
}
